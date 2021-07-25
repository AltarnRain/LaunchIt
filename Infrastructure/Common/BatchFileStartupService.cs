// <copyright file="BatchFileStartupService.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Common
{
    using Logic.Common;
    using Logic.Helpers;
    using System;
    using System.Diagnostics;
    using System.IO;

    /// <summary>
    /// Start an executable using a batchfile.
    /// </summary>
    /// <seealso cref="Logic.Common.IStartupService" />
    public class BatchFileStartupService : IStartupService
    {
        private readonly IConfigurationService configurationService;
        private readonly ILogger logger;
        private readonly IServiceHelper serviceHelper;
        private readonly IProcessHelper processHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="BatchFileStartupService" /> class.
        /// </summary>
        /// <param name="configurationService">The configuration service.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="serviceHelper">The service helper.</param>
        /// <param name="processHelper">The process helper.</param>
        public BatchFileStartupService(
            IConfigurationService configurationService,
            ILogger logger,
            IServiceHelper serviceHelper,
            IProcessHelper processHelper)
        {
            this.configurationService = configurationService;
            this.logger = logger;
            this.serviceHelper = serviceHelper;
            this.processHelper = processHelper;
        }

        /// <summary>
        /// Starts the specified executable.
        /// </summary>
        /// <param name="executablePath">The executable.</param>
        /// <param name="priorityClass">The priority class.</param>
        /// <returns>
        /// A Process.
        /// </returns>
        /// <exception cref="System.Exception">Failed to launch a process.</exception>
        public Process Start(string? executablePath, ProcessPriorityClass priorityClass)
        {
            var batchBuilder = new BatchBuilder();

            batchBuilder.Rem($"This file generated on {DateTime.Now}");
            batchBuilder.AddEmptyLine();

            var configuration = this.configurationService.Read();

            if (configuration.ShutdownExplorer)
            {
                // Shut down explorer before any other process is terminated.
                batchBuilder.Echo("Shutting down explorer. Your desktop will disappear. This is completely normal. Also, if your keyboard has volume keys and the like they will no longer work.");
                batchBuilder.Add("taskkill /f /im explorer.exe");
            }

            batchBuilder.Echo("Shutting down services...");
            foreach (var service in configuration.Services)
            {
                if (this.serviceHelper.IsRunning(service))
                {
                    this.logger.Log($"Adding shutdown command for service '{service}'");
                    batchBuilder.Add(GetServiceShutDownCommand(service));
                }
                else
                {
                    this.logger.Log($"Service '{service}' is not running. Skipping...");
                }
            }

            batchBuilder.Echo("Shutting down executables...");
            foreach (var exe in configuration.Executables)
            {
                this.logger.Log($"Adding kill command for executable {exe}");
                batchBuilder.Add(GetExecutableShutDownCommand(exe));
            }

            if (executablePath is not null)
            {
                var folder = Path.GetDirectoryName(executablePath);
                var executableName = Path.GetFileName(executablePath);

                if (folder == string.Empty)
                {
                    this.logger.Log($"Looks like you didn't specify a folder. No worries, I'll try to start {executableName}.");
                }
                else
                {
                    batchBuilder.Add(GetFolderCDCommand(folder));
                }

                batchBuilder.Add(GetExecutableExecutionCommand(executableName, priorityClass));
            }

            batchBuilder.Pause();

            // Reboot explorer if it was shut down.
            if (configuration.ShutdownExplorer)
            {
                batchBuilder.Add("explorer.exe");
            }

            var batchFileContent = batchBuilder.ToString();
            var batchRunner = new BatchRunner(batchFileContent, this.logger);

            var process = batchRunner.Run();

            if (process is null)
            {
                throw new System.Exception($"Failed to launch a process");
            }

            return process;
        }

        /// <summary>
        /// Gets the executable execution command.
        /// </summary>
        /// <param name="executableName">Name of the executable.</param>
        /// <param name="priorityClass">The priority class.</param>
        /// <returns>A startup command for the executable.</returns>
        private static string GetExecutableExecutionCommand(string executableName, ProcessPriorityClass priorityClass)
        {
            return $"start \"Game\" /{priorityClass.ToString().ToUpper()} {executableName}";
        }

        /// <summary>
        /// Gets the folder cd command.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <returns>A 'cd' command to go to a folder.</returns>
        private static string GetFolderCDCommand(string? folder)
        {
            return $"cd /d \"{folder}\"";
        }

        /// <summary>
        /// Gets the service shut down command.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <returns>Shutdown command for a service.</returns>
        private static string GetServiceShutDownCommand(string service)
        {
            return $"net stop \"{service}\"";
        }

        /// <summary>
        /// Gets the service shut down command.
        /// </summary>
        /// <param name="executable">The executable.</param>
        /// <returns>
        /// Shutdown command for an executable.
        /// </returns>
        private static string GetExecutableShutDownCommand(string executable)
        {
            return $"taskkill /f /im \"{executable}\"";
        }
    }
}
