// <copyright file="MonitorEventHandlerTests.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Logic.Handlers.Tests
{
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

            var model = new MonitoringEventModel
            {
                ProcessType = Domain.Types.ProcessType.Process,
                Name = "A name",
            };

            // Act
            target.HandleMonitoringEvent(model);

            // Assert
            var logs = scope.GetLogs();
            Assert.IsTrue(logs.Length > 0);
            var log1 = logs[0];

            Assert.IsTrue(log1.EndsWith("Started: Process 'A name'"));
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