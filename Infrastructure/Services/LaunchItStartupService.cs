// <copyright file="LaunchItStartupService.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Services
{
    using Domain.Models.Configuration;
    using Infrastructure.Helpers;
    using Logic.Contracts.Helpers;
    using Logic.Contracts.Services;
    using System;
    using System.Diagnostics;

    /// <summary>
    /// LaunchIt's own start service.
    /// </summary>
    /// <seealso cref="Logic.Contracts.Services.IStartupService" />
    public class LaunchItStartupService : IStartupService
    {
        private readonly IServiceHelper serviceHelper;
        private readonly IProcessHelper processHelper;
        private readonly ILogEventService logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchItStartupService" /> class.
        /// </summary>
        /// <param name="serviceHelper">The service helper.</param>
        /// <param name="processHelper">The process helper.</param>
        /// <param name="configurationService">The configuration service.</param>
        /// <param name="logger">The logger.</param>
        public LaunchItStartupService(
            IServiceHelper serviceHelper,
            IProcessHelper processHelper,
            ILogEventService logger)
        {
            this.serviceHelper = serviceHelper;
            this.processHelper = processHelper;
            this.logger = logger;
        }

        /// <summary>
        /// Starts the specified executable.
        /// </summary>
        /// <param name="launchModel">The launch model.</param>
        /// <returns>
        /// A Process.
        /// </returns>
        /// <exception cref="Exception">Should not start {executable}.</exception>
        public Process Start(LaunchModel launchModel)
        {
            if (launchModel.ShutdownExplorer)
            {
                this.logger.Log("Shutting down explorer. Your desktop will disappear.");
                this.logger.Log("This is completely normal!");
                this.logger.Log("If your keyboard has volume keys and the like they will no longer work. Explorer handles that.");

                this.processHelper.Stop("explorer.exe");
            }

            foreach (var service in launchModel.Services)
            {
                // This is the only time we do not track the shutdown count. Initial shutdown do not count towards the shutdown count.
                this.serviceHelper.Stop(service, false);
            }

            foreach (var exe in launchModel.Executables)
            {
                // This is the only time we do not track the shutdown count. Initial shutdown do not count towards the shutdown count.
                this.processHelper.Stop(exe, false);
            }

            var process = ProcessWrapper.Start(launchModel.ExecutableToLaunch);
            if (process is null)
            {
                throw new Exception($"Should not start {launchModel.ExecutableToLaunch}");
            }

            process.PriorityClass = launchModel.Priority;

            return process;
        }
    }
}
