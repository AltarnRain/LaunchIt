// <copyright file="BatchFileStartupService.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Common
{
    using Logic.Common;
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Start an executable using a batchfile.
    /// </summary>
    /// <seealso cref="Logic.Common.IStartupService" />
    public class BatchFileStartupService : IStartupService
    {
        private readonly IConfigurationService configurationService;

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="BatchFileStartupService"/> class.
        /// </summary>
        /// <param name="configurationService">The configuration service.</param>
        /// <param name="logger">The logger.</param>
        public BatchFileStartupService(IConfigurationService configurationService, ILogger logger)
        {
            this.configurationService = configurationService;
            this.logger = logger;
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
            foreach(var service in configuration.Services)
            {
                batchBuilder.Add(GetServiceShutDownCommand(service));
            }

            batchBuilder.Echo("Shutting down executables...");
            foreach(var exe in configuration.Executables)
            {
                batchBuilder.Add(GetExecutableShutDownCommand(exe));
            }

            if (executablePath is not null)
            {
                var folder = Path.GetDirectoryName(executablePath);
                var executableName = Path.GetFileName(executablePath);

                batchBuilder.Add(GetFolderCDCommand(folder));
                batchBuilder.Add(GetExecutableExecutionCommand(executableName, priorityClass));
            }

            batchBuilder.Pause();
            batchBuilder.Add("explorer.exe");

            var batchFileContent = batchBuilder.ToString();
            var batchRunner = new BatchRunner(batchFileContent);

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
