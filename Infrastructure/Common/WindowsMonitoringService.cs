// <copyright file="WindowsMonitoringService.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Common
{
    using Domain.Models.Common;
    using Logic.Common;
    using Logic.Helpers;
    using System;
    using System.IO;
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

        private Timer? timer;

        private string[]? runningServices;
        private string[]? runningProcesses;

        private string? serviceLogFile;
        private string? processLogFile;

        private StreamWriter? serviceLogFileWriter;
        private StreamWriter? processLogFileWriter;

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

            if(this.serviceLogFileWriter is null)
            {
                throw new Exception("No log writer for services... now that's weird.");
            }

            if (this.processLogFileWriter is null)
            {
                throw new Exception("No log writer for processes... now that's weird.");
            }

            if (this.serviceLogFile is null)
            {
                throw new Exception("No temp log file for services... now that's weird.");
            }

            if(this.processLogFile is null)
            {
                throw new Exception("No process log file either... now that's weird.");
            }

            this.timer.Stop();
            this.timer.Dispose();

            this.serviceLogFileWriter.Close();
            this.processLogFileWriter.Close();

            // Prepare a return value before clearing the state.
            var returnValue = new MonitoringResultModel
            {
                StartedProcesses = File.ReadAllLines(this.serviceLogFile).Distinct().ToArray(),
                StartedServices = File.ReadAllLines(this.processLogFile).Distinct().ToArray(),
            };

            // Reset.
            this.runningProcesses = null;
            this.runningServices = null;

            this.processLogFile = null;
            this.serviceLogFile = null;

            this.processLogFileWriter = null;
            this.serviceLogFileWriter = null;

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

            this.serviceLogFile = Path.GetTempFileName();
            this.processLogFile = Path.GetTempFileName();

            this.serviceLogFileWriter = File.AppendText(this.serviceLogFile);
            this.processLogFileWriter = File.AppendText(this.serviceLogFile);

            // Now we'll use a timer to check every X milli seconds if new processes or services were started.
            this.timer = new Timer(5000);
            this.timer.Elapsed += (sender, eventArgs) =>
            {
                this.logger.Log($"{DateTime.Now} monitoring activity...");

                var runningProcesses = this.processHelper.GetRunningProcesses();
                var newProcesses = runningProcesses.Where(rp => !this.runningProcesses.Contains(rp));
                this.serviceLogFileWriter.WriteLine(newProcesses);

                var runningServices = this.serviceHelper.GetRunningServices();
                var newServices = runningServices.Where(rp => !this.runningServices.Contains(rp));
                this.processLogFileWriter.WriteLine(newServices);
            };

            this.timer.Start();
            this.Monitoring = true;
        }
    }
}
