// <copyright file="WindowsMonitoringServiceTests.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Services.Tests
{
    using Domain.Exceptions;
    using global::Tests.Base;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            target.StartMonitoring();
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
            target.StartMonitoring();
            Assert.ThrowsException<MonitoringException>(() => target.StartMonitoring(), "Already monitoring.");
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
    }
}