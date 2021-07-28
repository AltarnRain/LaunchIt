// <copyright file="MonitorEventHandler.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Logic.Handlers
{
    using Domain.Models.Configuration;
    using Domain.Models.Events;
    using Logic.Helpers;
    using Logic.Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Class that handles monitoring events.
    /// </summary>
    public class MonitorEventHandler
    {
        private readonly IConfigurationService configurationService;
        private readonly ILogEventService logger;
        private readonly IServiceHelper serviceHelper;
        private readonly IProcessHelper processHelper;

        private readonly List<string> reportedIgnoredServices = new();
        private readonly List<string> reportedIgnoredProcesses = new();

        private ConfigurationModel? configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="MonitorEventHandler" /> class.
        /// </summary>
        /// <param name="configurationService">The configuration service.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="serviceHelper">The service helper.</param>
        /// <param name="processHelper">The process helper.</param>
        public MonitorEventHandler(
            IConfigurationService configurationService,
            ILogEventService logger,
            IServiceHelper serviceHelper,
            IProcessHelper processHelper)
        {
            this.configurationService = configurationService;
            this.logger = logger;
            this.serviceHelper = serviceHelper;
            this.processHelper = processHelper;
        }

        /// <summary>
        /// Handles the monitoring event.
        /// </summary>
        /// <param name="eventModel">The event model.</param>
        /// <exception cref="System.Exception">Unsupported process stype.</exception>
        public void HandleMonitoringEvent(MonitoringEventModel eventModel)
        {
            var configuration = this.GetCachedConfiguration();
            switch (eventModel.ProcessType)
            {
                case Domain.Types.ProcessType.Service:

                    this.HandleEvent(
                        eventModel.Name,
                        configuration.ServiceShutdownConfiguration.OnlyConfigured,
                        configuration.ServiceShutdownConfiguration.MaximumShutdownAttempts,
                        configuration.Services,
                        this.serviceHelper,
                        this.reportedIgnoredServices);

                    break;
                case Domain.Types.ProcessType.Process:

                    this.HandleEvent(
                        eventModel.Name,
                        configuration.ExecutableShutdownConfiguration.OnlyConfigured,
                        configuration.ExecutableShutdownConfiguration.MaximumShutdownAttempts,
                        configuration.Executables,
                        this.processHelper,
                        this.reportedIgnoredProcesses);

                    break;

                default:
                    throw new System.Exception($"{eventModel.ProcessType} is unhandled.");
            }
        }

        /// <summary>
        /// Handles the event for a service or executable that was (re)started.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="onlyConfigured">if set to <c>true</c> [only configured].</param>
        /// <param name="maximumShutdownAttempts">The maximum shutdown attempts.</param>
        /// <param name="configuredItems">The configured items.</param>
        /// <param name="stopHelper">The stop helper.</param>
        private void HandleEvent(
            string name,
            bool onlyConfigured,
            int maximumShutdownAttempts,
            string[] configuredItems,
            IStopHelper stopHelper,
            List<string> reportOnceList)
        {
            if (onlyConfigured && !configuredItems.Contains(name, StringComparer.OrdinalIgnoreCase))
            {
                if (!reportOnceList.Contains(name, StringComparer.OrdinalIgnoreCase))
                {
                    this.logger.Log($"{name} ignored. Not configured to shutdown.");
                    reportOnceList.Add(name);
                }

                return;
            }

            if (maximumShutdownAttempts != -1 && stopHelper.GetStopCount(name) >= maximumShutdownAttempts)
            {
                this.logger.Log($"{name} has been shut down the maximum amount of times.");
                return;
            }

            stopHelper.Stop(name);
        }

        private ConfigurationModel GetCachedConfiguration()
        {
            // Cache the configuration on a field to avoid IO when reading the config file and work when deserializing it.
            if (this.configuration is null)
            {
                this.configuration = this.configurationService.Read();
            }

            return this.configuration;
        }
    }
}
