// <copyright file="MonitorEventHandler.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Logic.Handlers
{
    using Domain.Models.Events;
    using Logic.Contracts.Services;

    /// <summary>
    /// Class that handles monitoring events.
    /// </summary>
    public class MonitorEventHandler
    {
        private readonly ILogEventService logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="MonitorEventHandler" /> class.
        /// </summary>
        /// <param name="configurationService">The configuration service.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="serviceHelper">The service helper.</param>
        /// <param name="processHelper">The process helper.</param>
        public MonitorEventHandler(
            ILogEventService logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Handles the monitoring event.
        /// </summary>
        /// <param name="eventModel">The event model.</param>
        /// <exception cref="System.Exception">Unsupported process stype.</exception>
        public void HandleMonitoringEvent(MonitoringEventModel eventModel)
        {
            var processType = eventModel.ProcessType.ToString();

            this.logger.Log($"Started: {processType} '{eventModel.Name}'");
        }
    }
}
