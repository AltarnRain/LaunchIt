// <copyright file="WindowsMonitoringService.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Services
{
    using Domain;
    using Domain.Models;
    using Logic.Helpers;
    using Logic.Services;
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
        private readonly ILoggerService logger;
        private readonly IServiceHelper serviceHelper;
        private readonly IProcessHelper processHelper;
        private readonly IConfigurationService configurationService;
        private readonly List<Action<MonitoringEventModel>> subscriptions = new();

        private Timer? timer;

        private string[]? runningServices;
        private string[]? runningProcesses;

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsMonitoringService" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="serviceHelper">The service helper.</param>
        /// <param name="processHelper">The process helper.</param>
        /// <param name="configurationService">The configuration service.</param>
        public WindowsMonitoringService(ILoggerService logger, IServiceHelper serviceHelper, IProcessHelper processHelper, IConfigurationService configurationService)
        {
            this.logger = logger;
            this.serviceHelper = serviceHelper;
            this.processHelper = processHelper;
            this.configurationService = configurationService;
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:Logic.Common.IMonitoringService" /> is monitoring.
        /// </summary>
        public bool Monitoring { get; private set; }

        /// <summary>
        /// Ends the monitoring.
        /// </summary>
        public void EndMonitoring()
        {
            if (this.Monitoring == false)
            {
                throw new Exception("Did not begin monitoring.");
            }

            if (this.timer is null)
            {
                throw new Exception("No timer running... now that's weird.");
            }

            this.timer.Stop();
            this.timer.Dispose();

            // Reset.
            this.runningProcesses = null;
            this.runningServices = null;
        }

        /// <summary>
        /// Starts the monitoring.
        /// </summary>
        /// <exception cref="Exception">Already monitoring.</exception>
        public void StartMonitoring()
        {
            if (this.Monitoring)
            {
                throw new Exception("Already monitoring.");
            }

            this.logger.Log($"Building monitoring snapshot...");

            // First thing. Map which processes and services are running.
            this.runningProcesses = this.processHelper.GetRunningProcesses();
            this.runningServices = this.serviceHelper.GetRunningServices();

            var configuration = this.configurationService.Read();

            // Now we'll use a timer to check every X milli seconds if new processes or services were started.
            this.timer = new Timer(configuration.MonitoringInterval);
            this.timer.Elapsed += (sender, eventArgs) =>
            {
                this.logger.Log($"{DateTime.Now} monitoring activity.... Next check at {DateTime.Now.AddMilliseconds(configuration.MonitoringInterval)}");

                var runningServices = this.serviceHelper.GetRunningServices();
                var runningProcesses = this.processHelper.GetRunningProcesses();

                var startedServices = GetArrayDifference(runningServices, this.runningServices)
                    .Select(s => new MonitoringEventModel { Name = s, ProcessType = ProcessType.Service });

                var startedProcesses = GetArrayDifference(runningProcesses, this.runningProcesses)
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

                this.runningServices = runningServices;
                this.runningProcesses = runningProcesses;
            };

            this.timer.Start();
            this.Monitoring = true;
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

        private static string[] GetArrayDifference(string[] currentItems, string[] orignalItems)
        {
            var newItems = currentItems.Where(rp => !orignalItems.Contains(rp));

            return newItems.ToArray();
        }
    }
}
