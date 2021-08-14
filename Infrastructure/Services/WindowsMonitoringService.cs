// <copyright file="WindowsMonitoringService.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Services
{
    using Domain.Exceptions;
    using Domain.Models.Configuration;
    using Domain.Models.Events;
    using Domain.Types;
    using Logic.Contracts.Helpers;
    using Logic.Contracts.Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Timers;

    /// <summary>
    /// Monitors servcices and executables that were started. Call <see cref="StartMonitoring"/> to begin, call <see cref="EndMonitoring"/> to finish.
    /// </summary>
    /// <seealso cref="IMonitoringService" />
    public class WindowsMonitoringService : IMonitoringService
    {
        private readonly ILogEventService logger;
        private readonly IServiceHelper serviceHelper;
        private readonly IProcessHelper processHelper;
        private readonly List<Action<MonitoringEventModel>> subscriptions = new();

        private Timer? timer;

        private string[]? serviceState;
        private string[]? executableState;
        private bool monitoring;

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsMonitoringService" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="serviceHelper">The service helper.</param>
        /// <param name="processHelper">The process helper.</param>
        /// <param name="configurationService">The configuration service.</param>
        public WindowsMonitoringService(
            ILogEventService logger,
            IServiceHelper serviceHelper,
            IProcessHelper processHelper)
        {
            this.logger = logger;
            this.serviceHelper = serviceHelper;
            this.processHelper = processHelper;
        }

        /// <summary>
        /// Ends the monitoring.
        /// </summary>
        public void EndMonitoring()
        {
            if (this.monitoring == false)
            {
                throw new MonitoringException("Did not begin monitoring.");
            }

            this.timer?.Stop();
            this.timer?.Close();
            this.timer?.Dispose();

            // Reset.
            this.executableState = null;
            this.serviceState = null;
        }

        /// <summary>
        /// Starts the monitoring.
        /// </summary>
        /// <param name="launchModel">The launch model.</param>
        /// <exception cref="MonitoringException">Already monitoring.</exception>
        public void StartMonitoring(LaunchModel launchModel)
        {
            if (this.monitoring)
            {
                throw new MonitoringException("Already monitoring.");
            }

            this.logger.Log($"Building monitoring snapshot...");

            // First thing. Map which processes and services are running. These collections are our Tabula Rasa. Anything new that
            // that is started will be detected and shut down. For the executable state I'm purposely ommitting
            // executables configured for shut down. Getting 'running' executables can be a bit flaky and
            // on occasion I noticed an executable in the process of being killed was added to this.executableState.
            // By ensuring the executableState does not contain executables the user wishes to shut down we hand
            // over the responsibility of killing them to the monitoring process. This, in addition, also
            // makes it clear which executables are troublesome when it comes to being shut down.
            this.executableState = this.processHelper
                .GetRunningExecutables()
                .Where(e => !launchModel.Executables.Contains(e, StringComparer.OrdinalIgnoreCase))
                .ToArray();

            this.serviceState = this.serviceHelper
                .GetRunningServices();

            // Now we'll use a timer to check every X milli seconds if new processes or services were started.
            this.timer = new Timer(launchModel.MonitoringInterval);
            this.logger.Log($"Monitoring activity.");

            this.timer.Elapsed += (sender, eventArgs) =>
            {
                var runningServices = this.serviceHelper.GetRunningServices();
                var runningProcesses = this.processHelper.GetRunningExecutables();

                var startedServices = runningServices.Except(this.serviceState)
                    .Select(s => new MonitoringEventModel { Name = s, ProcessType = ProcessType.Service });

                var startedProcesses = runningProcesses.Except(this.executableState)
                    .Select(s => new MonitoringEventModel { Name = s, ProcessType = ProcessType.Process });

                var allEvents = new List<MonitoringEventModel>(startedServices);
                allEvents.AddRange(startedProcesses);

                // Notify all subscribers there's been an event.
                foreach (var subscription in this.subscriptions)
                {
                    foreach (var e in allEvents)
                    {
                        subscription(e);
                    }
                }
            };

            this.timer.Start();
            this.monitoring = true;
        }

        /// <summary>
        /// Subscribes the specified subscription.
        /// </summary>
        /// <param name="subscription">The subscription.</param>
        /// <returns>An action to remove the subscription.</returns>
        public Action Subscribe(Action<MonitoringEventModel> subscription)
        {
            this.subscriptions.Add(subscription);

            return () => this.subscriptions.Remove(subscription);
        }
    }
}
