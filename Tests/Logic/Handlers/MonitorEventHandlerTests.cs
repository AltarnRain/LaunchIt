// <copyright file="MonitorEventHandlerTests.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Logic.Handlers.Tests
{
    using Domain.Models.Configuration;
    using Domain.Models.Events;
    using global::Tests.Base;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests <see cref="MonitorEventHandler"/>.
    /// </summary>
    [TestClass]
    public class MonitorEventHandlerTests : TestBase
    {
        /// <summary>
        /// Handles the monitoring event test.
        /// </summary>
        [TestMethod]
        public void HandleMonitoringEventProcessTest()
        {
            // Arrange
            var scope = this.StartTestScopeForMonitorEventHandler();
            var target = scope.Get<MonitorEventHandler>();

            var configuration = new ConfigurationModel
            {
                ExecutableShutdownConfiguration = new ShutdownConfigurationModel
                {
                    OnlyConfigured = false,
                    ShutdownAfterRestart = true,
                    MaximumShutdownAttempts = 1,
                },
            };

            scope.SetConfiguration(configuration);

            var model = new MonitoringEventModel
            {
                ProcessType = Domain.Types.ProcessType.Process,
                Name = "A name",
            };

            // Act
            target.HandleMonitoringEvent(model);

            // Assert
            var processStopService = scope.GetTestProcessHelper();
            Assert.AreEqual(1, processStopService.StopCalls.Count);

            var serviceStopService = scope.GetTestServiceHelper();
            Assert.AreEqual(0, serviceStopService.StopCalls.Count);
        }

        /// <summary>
        /// Handles the monitoring event test.
        /// </summary>
        [TestMethod]
        public void HandleMonitoringEventServiceTest()
        {
            // Arrange
            var scope = this.StartTestScopeForMonitorEventHandler();
            var target = scope.Get<MonitorEventHandler>();

            var configuration = new ConfigurationModel
            {
                ServiceShutdownConfiguration = new ShutdownConfigurationModel
                {
                    OnlyConfigured = false,
                    ShutdownAfterRestart = true,
                    MaximumShutdownAttempts = 1,
                },
            };

            scope.SetConfiguration(configuration);

            var model = new MonitoringEventModel
            {
                ProcessType = Domain.Types.ProcessType.Service,
                Name = "A name",
            };

            // Act
            target.HandleMonitoringEvent(model);

            // Assert
            var serviceStopService = scope.GetTestServiceHelper();
            Assert.AreEqual(1, serviceStopService.StopCalls.Count);
        }

        /// <summary>
        /// Handles the monitoring event test.
        /// </summary>
        [TestMethod]
        public void HandleMonitoringEventServiceOnlyConfiguredTest()
        {
            // Arrange
            var scope = this.StartTestScopeForMonitorEventHandler();
            var target = scope.Get<MonitorEventHandler>();

            var configuration = new ConfigurationModel
            {
                ServiceShutdownConfiguration = new ShutdownConfigurationModel
                {
                    OnlyConfigured = true,
                    MaximumShutdownAttempts = 1,
                    ShutdownAfterRestart = true,
                },
                Services = new[]
                {
                    "A name",
                },
            };

            scope.SetConfiguration(configuration);

            var model1 = new MonitoringEventModel
            {
                ProcessType = Domain.Types.ProcessType.Service,
                Name = "A name",
            };

            var model2 = new MonitoringEventModel
            {
                ProcessType = Domain.Types.ProcessType.Service,
                Name = "Another name",
            };

            // Act
            target.HandleMonitoringEvent(model1);
            target.HandleMonitoringEvent(model2);

            // Assert
            var serviceStopService = scope.GetTestServiceHelper();
            Assert.AreEqual(1, serviceStopService.StopCalls.Count);
        }

        /// <summary>
        /// Handles the monitoring event test.
        /// </summary>
        [TestMethod]
        public void HandleMonitoringEventServiceOnlyConfiguredMaxAttemptsTest()
        {
            // Arrange
            var scope = this.StartTestScopeForMonitorEventHandler();
            var target = scope.Get<MonitorEventHandler>();

            var configuration = new ConfigurationModel
            {
                ServiceShutdownConfiguration = new ShutdownConfigurationModel
                {
                    OnlyConfigured = true,
                    MaximumShutdownAttempts = 2,
                    ShutdownAfterRestart = true,
                },
                Services = new[]
                {
                    "A name",
                },
            };

            scope.SetConfiguration(configuration);

            var model = new MonitoringEventModel
            {
                ProcessType = Domain.Types.ProcessType.Service,
                Name = "A name",
            };

            // Act
            target.HandleMonitoringEvent(model);
            target.HandleMonitoringEvent(model);
            target.HandleMonitoringEvent(model); // Max stop attempts reached. No effect other then logging.
            target.HandleMonitoringEvent(model); // Max stop attempts reached. No effect other then logging.

            // Assert
            var serviceStopService = scope.GetTestServiceHelper();
            Assert.AreEqual(2, serviceStopService.StopCalls.Count);

            var logs = scope.GetLogs();
            Assert.AreEqual(2, logs.Length);
        }

        /// <summary>
        /// Starts the test scope for monitor event handler.
        /// </summary>
        /// <returns>A test scope setup to test MonitorEventHandler.</returns>
        private TestScope StartTestScopeForMonitorEventHandler()
        {
            // Arrange
            return this.StartTestScope(new[]
            {
                new BindModel
                {
                    ServiceType = typeof(MonitorEventHandler),
                },
            });
        }
    }
}