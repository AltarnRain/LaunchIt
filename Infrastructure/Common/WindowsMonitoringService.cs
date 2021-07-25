// <copyright file="WindowsMonitoringService.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Common
{
    using Domain.Models.Common;
    using Logic.Common;
    using Logic.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Timers;

    /// <summary>
    /// Monitors servcices and executables that were started.
    /// </summary>
    /// <seealso cref="Logic.Common.IMonitoringService" />
    public class WindowsMonitoringService : IMonitoringService
    {
        private readonly ILogger logger;
        private readonly IServiceHelper serviceHelper;
        private readonly IProcessHelper processHelper;
        private readonly List<string> startedServices = new();
        private readonly List<string> startedProcesses = new();

        private Timer? timer;

        private string[]? runningProcesses;
        private string[]? runningServices;

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsMonitoringService" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="serviceHelper">The service helper.</param>
        /// <param name="processHelper">The process helper.</param>
        public WindowsMonitoringService(ILogger logger, IServiceHelper serviceHelper, IProcessHelper processHelper)
        {
            this.logger = logger;
            this.serviceHelper = serviceHelper;
            this.processHelper = processHelper;
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:Logic.Common.IMonitoringService" /> is monitoring.
        /// </summary>
        public bool Monitoring { get; private set; }

        /// <summary>
        /// Ends the monitoring.
        /// </summary>
        /// <returns>
        /// A list of names of things that were restarted while monitoring.
        /// </returns>
        public MonitoringResultModel EndMonitoring()
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

            // Prepare a return value before clearing the state.
            var returnValue = new MonitoringResultModel
            {
                StartedProcesses = this.startedProcesses.Distinct().ToArray(),
                StartedServices = this.startedServices.Distinct().ToArray(),
            };

            // Reset.
            this.runningProcesses = null;
            this.runningServices = null;
            this.startedServices.Clear();
            this.startedProcesses.Clear();

            return returnValue;
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

            // Now we'll use a timer to check every X milli seconds if new processes or services were started.
            this.timer = new Timer(5000);
            this.timer.Elapsed += (sender, eventArgs) =>
            {
                this.logger.Log($"{DateTime.Now} monitoring activity...");

                var runningProcesses = this.processHelper.GetRunningProcesses();
                var newProcesses = runningProcesses.Where(rp => !this.runningProcesses.Contains(rp));
                this.startedProcesses.AddRange(newProcesses);

                var runningServices = this.serviceHelper.GetRunningServices();
                var newServices = runningServices.Where(rp => !this.runningServices.Contains(rp));
                this.startedServices.AddRange(newServices);
            };

            this.timer.Start();
            this.Monitoring = true;
        }
    }
}
