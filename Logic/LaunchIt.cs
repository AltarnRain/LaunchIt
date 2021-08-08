// <copyright file="LaunchIt.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Logic
{
    using Domain.Models.Configuration;
    using Logic.Contracts.Helpers;
    using Logic.Contracts.Services;
    using Logic.Extensions;
    using Logic.Handlers;
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Startup class for the program.
    /// </summary>
    [ExcludeFromCodeCoverage(Justification = "Top level class for 'Logic'. I'd have to abstract everything to the point this code does nothing.")]
    public class LaunchIt
    {
        private readonly IConfigurationService configurationService;
        private readonly ILogEventService logger;
        private readonly IStartupService startupService;
        private readonly IMonitoringService monitoringService;
        private readonly IProcessHelper processHelper;
        private readonly IServiceHelper serviceHelper;
        private readonly IConfigurationValidationService configurationValidationService;
        private readonly IEditorService editorService;

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchIt" /> class.
        /// </summary>
        /// <param name="configurationService">The configuration service.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="startupService">The startup service.</param>
        /// <param name="monitoringService">The monitoring service.</param>
        /// <param name="processHelper">The process helper.</param>
        /// <param name="serviceHelper">The service helper.</param>
        /// <param name="configurationValidationService">The configuration validation service.</param>
        /// <param name="editorService">The editor service.</param>
        /// <param name="pathProvider">The path provider.</param>
        public LaunchIt(
            IConfigurationService configurationService,
            ILogEventService logger,
            IStartupService startupService,
            IMonitoringService monitoringService,
            IProcessHelper processHelper,
            IServiceHelper serviceHelper,
            IConfigurationValidationService configurationValidationService,
            IEditorService editorService)
        {
            this.configurationService = configurationService;
            this.logger = logger;
            this.startupService = startupService;
            this.monitoringService = monitoringService;
            this.processHelper = processHelper;
            this.serviceHelper = serviceHelper;
            this.configurationValidationService = configurationValidationService;
            this.editorService = editorService;
        }

        /// <summary>
        /// Starts the specified arguments.
        /// </summary>
        /// <param name="launchModel">The launch model.</param>
        public void Start(LaunchModel launchModel)
        {
            var didWork = this.CheckForConfigurationFile();

            if (didWork)
            {
                this.editorService.EditConfiguration();
                return;
            }

            if (string.IsNullOrWhiteSpace(launchModel.ExecutableToLaunch))
            {
                this.logger.Log("You didn't give me anything to start.");
                return;
            }

            if (launchModel.Services.Length + launchModel.Executables.Length == 0)
            {
                this.logger.Log("You didn't configure and services or executables for me to shut down.");
            }

            var configuration = this.configurationService.Read();

            foreach (var message in this.configurationValidationService.Validate(configuration))
            {
                this.logger.Log(message);
            }

            var process = this.startupService.Start(launchModel);

            // User wants a batchfile so we're done and can exit.
            if (launchModel.UseBatchFile)
            {
                return;
            }

            Action? unsubscribeMonitorEventHandler = null;
            if (launchModel.StartMonitoring())
            {
                this.monitoringService.StartMonitoring();

                var monitorEventHandler = new MonitorEventHandler(
                    this.configurationService,
                    this.logger,
                    this.serviceHelper,
                    this.processHelper);

                unsubscribeMonitorEventHandler = this.monitoringService.Subscribe(monitorEventHandler.HandleMonitoringEvent);
            }

            if (process is null)
            {
                this.logger.Log($"{launchModel.ExecutableToLaunch} did not start.");
                return;
            }

            process.WaitForExit();

            this.logger.Log($"{launchModel.ExecutableToLaunch} has shutdown");

            // Clear subscriptions.
            unsubscribeMonitorEventHandler?.Invoke();

            if (configuration.ShutdownExplorer)
            {
                this.processHelper.Start(Domain.Constants.KnownProcesses.ExplorerExe);
            }

            // End monitoring if its running.
            if (this.monitoringService.Monitoring)
            {
                this.monitoringService.EndMonitoring();
            }
        }

        private bool CheckForConfigurationFile()
        {
            if (!this.configurationService.ConfigurationFileExists())
            {
                this.configurationService.WriteExampleConfigurationFile();
                this.logger.Log($"Looks like you're starting me for the first time. I'll setup an example configuration file and open it in notepad.");
                this.logger.Log($"If you want to to edit your configuration file just run LaunchIt with the 'edit' switch. For example:");
                this.logger.Log($"   LaunchIt {SwitchCommands.Edit.GetCommandLineArgument()}");
                return true;
            }

            return false;
        }
    }
}
