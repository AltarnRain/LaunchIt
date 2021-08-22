// <copyright file="WindowsMonitoringServiceTests.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Services.Tests
{
    using Domain.Exceptions;
    using Domain.Models.Configuration;
    using Domain.Models.Events;
    using global::Tests.Base;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        /// Tests if the <see cref="WindowsMonitoringService"/> returns an event for services and processes that it thinks
        /// have started.
        /// </summary>
        [TestMethod]
        public void DetectsStartupsTest()
        {
            // Arrange
            using var scope = this.StartTestScope(typeof(WindowsMonitoringService));
            var target = scope.Get<WindowsMonitoringService>();

            var testTimerFactory = scope.GetTestTimerFactory();
            var testProcessHelper = scope.GetTestProcessHelper();
            var testServiceHelper = scope.GetTestServiceHelper();

            // Keep track how often GetRunningExecutables is called so we can return an array with some values
            // on the second call.
            var processCallCount = 0;
            testProcessHelper.HandleGetRunningExecutables = () =>
            {
                processCallCount++;
                if (processCallCount == 2)
                {
                    return new[] { "Exe 1", "Exe 2", "Exe 3" };
                }

                return new[] { "Exe 3" };
            };

            var serviceCallCount = 0;
            testServiceHelper.HandleGetRunningServices = () =>
            {
                serviceCallCount++;
                if (serviceCallCount == 2)
                {
                    return new[] { "Service 1", "Service 2", "Service 3" };
                }

                return new[] { "Service 3" };
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

            timerEvent.Invoke(null, null);

            Assert.IsTrue(monitoringEvents.Any(me => me.Name == "Exe 1" && me.ProcessType == Domain.Types.ProcessType.Process));
            Assert.IsTrue(monitoringEvents.Any(me => me.Name == "Exe 2" && me.ProcessType == Domain.Types.ProcessType.Process));
            Assert.IsFalse(monitoringEvents.Any(me => me.Name == "Exe 3"));

            Assert.IsTrue(monitoringEvents.Any(me => me.Name == "Service 1" && me.ProcessType == Domain.Types.ProcessType.Service));
            Assert.IsTrue(monitoringEvents.Any(me => me.Name == "Service 2" && me.ProcessType == Domain.Types.ProcessType.Service));
            Assert.IsFalse(monitoringEvents.Any(me => me.Name == "Service 3"));
        }
    }
}