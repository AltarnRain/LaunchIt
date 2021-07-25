// <copyright file="WindowsMonitoringService.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Services
{
    using Domain.Models.Common;
    using Logic.Helpers;
    using Logic.Services;
    using System;
    using System.IO;
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

            if (this.serviceLogFileWriter is null)
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

            if (this.processLogFile is null)
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
                StartedServices = File.ReadAllLines(this.serviceLogFile).Distinct().ToArray(),
                StartedProcesses = File.ReadAllLines(this.processLogFile).Distinct().ToArray(),
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
            this.processLogFileWriter = File.AppendText(this.processLogFile);

            var configuration = this.configurationService.Read();

            // Now we'll use a timer to check every X milli seconds if new processes or services were started.
            this.timer = new Timer(configuration.MonitoringInterval);
            this.timer.Elapsed += (sender, eventArgs) =>
            {
                this.logger.Log($"{DateTime.Now} monitoring activity.... Next check at {DateTime.Now.AddMilliseconds(configuration.MonitoringInterval)}");

                var runningServices = this.serviceHelper.GetRunningServices();
                var runningProcesses = this.processHelper.GetRunningProcesses();

                LogMonitorResults(runningServices, this.runningServices, this.serviceLogFileWriter);
                LogMonitorResults(runningProcesses, this.runningProcesses, this.processLogFileWriter);
            };

            this.timer.Start();
            this.Monitoring = true;
        }

        private static void LogMonitorResults(string[] currentItems, string[] orignalItems, StreamWriter streamWriter)
        {
            var newItems = currentItems.Where(rp => !orignalItems.Contains(rp));

            foreach (var item in newItems)
            {
                streamWriter.WriteLine(item);
            }

            streamWriter.Flush();
        }
    }
}
