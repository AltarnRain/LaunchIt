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
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Startup class for the program.
    /// </summary>
    [ExcludeFromCodeCoverage(Justification = "Top level class for 'Logic'. I'd have to abstract everything to the point this code does nothing.")]
    public class LaunchIt
    {
        private readonly ILogEventService logger;
        private readonly IMonitoringService monitoringService;
        private readonly IProcessHelper processHelper;
        private readonly IConfigurationValidationService configurationValidationService;
        private readonly IStartupServiceFactory startupServiceFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchIt" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="monitoringService">The monitoring service.</param>
        /// <param name="processHelper">The process helper.</param>
        /// <param name="configurationValidationService">The configuration validation service.</param>
        /// <param name="editorService">The editor service.</param>
        /// <param name="startupServiceFactory">The startup service factory.</param>
        public LaunchIt(
            ILogEventService logger,
            IMonitoringService monitoringService,
            IProcessHelper processHelper,
            IConfigurationValidationService configurationValidationService,
            IStartupServiceFactory startupServiceFactory)
        {
            this.logger = logger;
            this.monitoringService = monitoringService;
            this.processHelper = processHelper;
            this.configurationValidationService = configurationValidationService;
            this.startupServiceFactory = startupServiceFactory;
        }

        /// <summary>
        /// Starts the specified arguments.
        /// </summary>
        /// <param name="launchModel">The launch model.</param>
        public void Start(LaunchModel launchModel)
        {
            if (string.IsNullOrWhiteSpace(launchModel.ExecutableToLaunch))
            {
                this.logger.Log("You didn't give me anything to start.");
                return;
            }

            if (launchModel.Services.Length + launchModel.Executables.Length == 0)
            {
                this.logger.Log("You didn't configure and services or executables for me to shut down.");
            }

            foreach (var message in this.configurationValidationService.Validate())
            {
                this.logger.Log(message);
            }

            var startupService = this.startupServiceFactory.Create(launchModel);
            var process = startupService.Start(launchModel);

            // User wants a batchfile so we're done and can exit.
            if (launchModel.UseBatchFile)
            {
                return;
            }

            this.monitoringService.StartMonitoring(launchModel);

            var monitorEventHandler = new MonitorEventHandler(this.logger);

            var unsubscribeMonitorEventHandler = this.monitoringService.Subscribe(monitorEventHandler.HandleMonitoringEvent);

            if (process is null)
            {
                this.logger.Log($"{launchModel.ExecutableToLaunch} did not start.");
                return;
            }

            process.WaitForExit();

            this.logger.Log($"{launchModel.ExecutableToLaunch} has shutdown");

            // Clear subscriptions.
            unsubscribeMonitorEventHandler?.Invoke();

            if (launchModel.ShutdownExplorer)
            {
                this.processHelper.Start(Domain.Constants.KnownProcesses.ExplorerExe);
            }

            // End monitoring if its running.
            if (this.monitoringService.Monitoring)
            {
                this.monitoringService.EndMonitoring();
            }
        }
    }
}
