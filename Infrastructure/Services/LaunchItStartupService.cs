// <copyright file="LaunchItStartupService.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Services
{
    using Logic.Helpers;
    using Logic.Services;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading.Tasks;

    /// <summary>
    /// LaunchIt's own start service.
    /// </summary>
    /// <seealso cref="Logic.Services.IStartupService" />
    public class LaunchItStartupService : IStartupService
    {
        private readonly IServiceHelper serviceHelper;
        private readonly IProcessHelper processHelper;
        private readonly IConfigurationService configurationService;
        private readonly ILoggerService logger;

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
            IConfigurationService configurationService,
            ILoggerService logger)
        {
            this.serviceHelper = serviceHelper;
            this.processHelper = processHelper;
            this.configurationService = configurationService;
            this.logger = logger;
        }

        /// <summary>
        /// Starts the specified executable.
        /// </summary>
        /// <param name="executable">The executable.</param>
        /// <returns>
        /// A Process.
        /// </returns>
        public Process Start(string executable)
        {
            var configuration = this.configurationService.Read();

            if (configuration.ShutdownExplorer)
            {
                this.logger.Log("Shutting down explorer. Your desktop will disappear.");
                this.logger.Log("This is completely normal!");
                this.logger.Log("If your keyboard has volume keys and the like they will no longer work. Explorer handles that.");

                this.processHelper.Stop("explorer.exe");
            }

            foreach (var service in configuration.Services)
            {
                // This is the only time we do not track the shutdown count. Initial shutdown do not count towards the shutdown count.
                this.serviceHelper.Stop(service, false);
            }

            foreach (var exe in configuration.Executables)
            {
                // This is the only time we do not track the shutdown count. Initial shutdown do not count towards the shutdown count.
                this.processHelper.Stop(exe, false);
            }

            var processStartInfo = new ProcessStartInfo
            {
                UseShellExecute = true,
                FileName = executable,
            };

            var process = Process.Start(processStartInfo);
            if (process is null)
            {
                throw new Exception($"Should not start {executable}");
            }

            var priorityClass = Enum.Parse<ProcessPriorityClass>(configuration.Priority, true);
            process.PriorityClass = priorityClass;

            return process;
        }
    }
}
