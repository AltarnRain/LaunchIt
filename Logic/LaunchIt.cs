// <copyright file="LaunchIt.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Logic
{
    using Logic.Extensions;
    using Logic.Handlers;
    using Logic.Helpers;
    using Logic.Logging;
    using Logic.Services;
    using System;
    using System.IO;

    /// <summary>
    /// Startup class for the program.
    /// </summary>
    public class LaunchIt
    {
        private readonly IConfigurationService configurationService;
        private readonly ILoggerService logger;
        private readonly IStartupService startupService;
        private readonly IMonitoringService monitoringService;
        private readonly IProcessHelper processHelper;
        private readonly IServiceHelper serviceHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchIt" /> class.
        /// </summary>
        /// <param name="configurationService">The configuration service.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="startupService">The startup service.</param>
        /// <param name="monitoringService">The monitoring service.</param>
        /// <param name="memoryCleaningService">The memory cleaning service.</param>
        /// <param name="processHelper">The process helper.</param>
        /// <param name="serviceHelper">The service helper.</param>
        public LaunchIt(
            IConfigurationService configurationService,
            ILoggerService logger,
            IStartupService startupService,
            IMonitoringService monitoringService,
            IProcessHelper processHelper,
            IServiceHelper serviceHelper)
        {
            this.configurationService = configurationService;
            this.logger = logger;
            this.startupService = startupService;
            this.monitoringService = monitoringService;
            this.processHelper = processHelper;
            this.serviceHelper = serviceHelper;
        }

        /// <summary>
        /// Starts the specified arguments.
        /// </summary>
        /// <param name="executable">The executable.</param>
        public void Start(string executable)
        {
            var didWork = this.CheckForConfigurationFile();

            if (didWork)
            {
                this.configurationService.EditInNotepad();
                return;
            }

            if (string.IsNullOrWhiteSpace(executable))
            {
                this.logger.Log("You didn't give me anything to start.");
                this.ManualShutdown();
                return;
            }

            var configuration = this.configurationService.Read();

            if (configuration.Services.Length + configuration.Executables.Length == 0)
            {
                this.logger.Log("You didn't configure and services or executables for me to shut down.");
            }

            var process = this.startupService.Start(executable);

            // User wants a batchfile so we're done and can exit.
            if (configuration.UseBatchFile)
            {
                return;
            }

            // Save everything the ILoggerService logs to a file.
            var logFile = Path.GetTempFileName() + ".txt";
            var fileLogger = new FileLogger(logFile);
            var unsubscribeFileLogger = this.logger.Subscribe(fileLogger.Log);

            Action? unsubscribeMonitorEventHandler = null;
            if (configuration.StartMonitoring())
            {
                this.monitoringService.StartMonitoring();

                var monitorEventHandler = new MonitorEventHandler(
                    this.configurationService,
                    this.logger,
                    this.serviceHelper,
                    this.processHelper);

                unsubscribeMonitorEventHandler = this.monitoringService.Subscribe(monitorEventHandler.HandleMonitoringEvent);
            }

            process.WaitForExit();

            // Clear subscriptions.
            unsubscribeMonitorEventHandler?.Invoke();
            unsubscribeFileLogger();

            // Close the file logger and write whatever is in the log cache to disk.
            fileLogger.Close();

            if (configuration.ShutdownExplorer)
            {
                this.processHelper.Start("explorer.exe");
            }

            // End monitoring if its running.
            if (this.monitoringService.Monitoring)
            {
                this.monitoringService.EndMonitoring();
            }

            this.processHelper.Start(logFile);

            if (!configuration.ShutDownWhenProgramCloses)
            {
                this.ManualShutdown();
            }
            else
            {
                this.logger.Log("Program stopped. Shutting down LaunchIt.");
            }
        }

        private void ManualShutdown()
        {
            var manualResetEvent = new System.Threading.ManualResetEvent(false);
            this.logger.Log("Click 'X' to close the program.");
            manualResetEvent.WaitOne();
        }

        private bool CheckForConfigurationFile()
        {
            if (!this.configurationService.ConfigurationFileExists())
            {
                this.configurationService.WriteExampleConfigurationFile();
                this.logger.Log($"Looks like you're starting LaunchIt for the first time. I'll setup an example configuration file and open it in notepad.");
                this.logger.Log($"If you want to to edit your configuration file just run LaunchIt with the 'edit' argument. For example:");
                this.logger.Log($"   LaunchIt edit");
                return true;
            }

            return false;
        }
    }
}
