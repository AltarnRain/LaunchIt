// <copyright file="WindowsMonitoringServiceTests.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Services.Tests
{
    using Domain.Exceptions;
    using Domain.Models.Configuration;
    using Domain.Models.Events;
    using global::Tests.Base;
    using Infrastructure.Contracts.Factories;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Tests the <see cref="WindowsMonitoringService"/>.
    /// </summary>
    /// <seealso cref="Tests.Base.TestBase" />
    [TestClass]
    public class WindowsMonitoringServiceTests : TestBase
    {
        /// <summary>
        /// Starts the monitoring test.
        /// </summary>
        [TestMethod]
        public void StartAndEndMonitoringTest()
        {
            // Arrange
            using var scope = this.StartTestScope(typeof(WindowsMonitoringService));
            var target = scope.Get<WindowsMonitoringService>();

            // Act & Assert. Should not throw.
            target.StartMonitoring(new LaunchModel { MonitoringInterval = 5000 });
            target.EndMonitoring();
        }

        /// <summary>
        /// Ends the monitoring before starting test.
        /// </summary>
        [TestMethod]
        public void EndMonitoringBeforeStartingTest()
        {
            // Arrange
            using var scope = this.StartTestScope(typeof(WindowsMonitoringService));
            var target = scope.Get<WindowsMonitoringService>();

            // Act
            Assert.ThrowsException<MonitoringException>(() => target.EndMonitoring(), "Did not begin monitoring.");
        }

        /// <summary>
        /// Ends the monitoring before starting test.
        /// </summary>
        [TestMethod]
        public void StartMonitoringTwiceTest()
        {
            // Arrange
            using var scope = this.StartTestScope(typeof(WindowsMonitoringService));
            var target = scope.Get<WindowsMonitoringService>();

            // Act
            target.StartMonitoring(new LaunchModel { MonitoringInterval = 5000 });
            Assert.ThrowsException<MonitoringException>(() => target.StartMonitoring(new LaunchModel()), "Already monitoring.");
        }

        /// <summary>
        /// Ends the monitoring before starting test.
        /// </summary>
        [TestMethod]
        public void SubscribeTest()
        {
            // Arrange
            using var scope = this.StartTestScope(typeof(WindowsMonitoringService));
            var target = scope.Get<WindowsMonitoringService>();

            // Act
            var unsubscribe = target.Subscribe((v) => Assert.IsNotNull(v));

            // Assert
            Assert.IsNotNull(unsubscribe);
            unsubscribe(); // Should not throw.
        }

        /// <summary>
        /// Detectses the new services.
        /// </summary>
        [TestMethod]
        public void DetectsNewServices()
        {
            // Arrange
            using var scope = this.StartTestScope(typeof(WindowsMonitoringService));
            var target = scope.Get<WindowsMonitoringService>();

            var testTimerFactory = scope.GetTestTimerFactory();
            var testProcessHelper = scope.GetTestProcessHelper();

            // Keep track how often GetRunningExecutables is called so we can return an array with some values
            // on the second call.
            var callCount = 0;

            testProcessHelper.HandleGetRunningExecutables = () =>
            {
                callCount++;
                if (callCount == 1)
                {
                    // Exe 3 is already running. It should not trigger a monitoring event.
                    return new[] { "Exe 3" };
                }

                if (callCount == 2)
                {
                    return new[] { "Exe 1", "Exe 2", "Exe 3" };
                }

                return Array.Empty<string>();
            };

            var launchModel = new LaunchModel();

            // Act
            target.StartMonitoring(launchModel);

            // Assert
            var timerEvent = testTimerFactory.Events.First();

            var monitoringEvents = new List<MonitoringEventModel>();

            // Subscribe to changes.
            target.Subscribe((monitoringEventModel) =>
            {
                monitoringEvents.Add(monitoringEventModel);
            });

            // Call once to set the monitoring service state.
            timerEvent.Invoke(null, null);

            Assert.IsTrue(monitoringEvents.Any(me => me.Name == "Exe 1"));
            Assert.IsTrue(monitoringEvents.Any(me => me.Name == "Exe 2"));
            Assert.IsFalse(monitoringEvents.Any(me => me.Name == "Exe 3"));
        }
    }
}