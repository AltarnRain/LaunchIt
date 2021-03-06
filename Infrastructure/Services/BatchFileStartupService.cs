// <copyright file="BatchFileStartupService.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Services
{
    using Domain.Models.Configuration;
    using Infrastructure.Contracts.Factories;
    using Infrastructure.Helpers;
    using Logic.Contracts.Services;
    using System;
    using System.Diagnostics;
    using System.IO;

    /// <summary>
    /// Start an executable using a batchfile.
    /// </summary>
    /// <seealso cref="Logic.Contracts.Services.IStartupService" />
    /// <seealso cref="IStartupService" />
    public class BatchFileStartupService : IStartupService
    {
        private readonly ILogEventService logger;
        private readonly IBatchRunnerFactory batchRunnerFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="BatchFileStartupService" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="batchRunnerFactory">The batch runner factory.</param>
        public BatchFileStartupService(ILogEventService logger, IBatchRunnerFactory batchRunnerFactory)
        {
            this.logger = logger;
            this.batchRunnerFactory = batchRunnerFactory;
        }

        /// <summary>
        /// Starts the specified executable.
        /// </summary>
        /// <param name="launchModel">The launch model.</param>
        /// <returns>
        /// A Process.
        /// </returns>
        public Process? Start(LaunchModel launchModel)
        {
            if (launchModel.ExecutableToLaunch == string.Empty)
            {
                this.logger.Log($"Nothing to run");
                return null;
            }

            var batchBuilder = new BatchBuilder();

            batchBuilder.Rem($"This file generated on {DateTime.Now}");
            batchBuilder.AddEmptyLine();

            if (launchModel.ShutdownExplorer)
            {
                // Shut down explorer before any other process is terminated.
                batchBuilder.Echo("Shutting down explorer. Your desktop will disappear.");
                batchBuilder.Echo("This is completely normal!");
                batchBuilder.Echo("If your keyboard has volume keys and the like they will no longer work. Explorer handles that.");
                batchBuilder.Add($"taskkill /f /im {Domain.Constants.KnownProcesses.ExplorerExe}");
            }

            batchBuilder.Echo("Shutting down services...");
            foreach (var service in launchModel.Services)
            {
                this.logger.Log($"Adding shutdown command for service '{service}'");
                batchBuilder.Add(GetServiceShutDownCommand(service));
            }

            batchBuilder.Echo("Shutting down executables...");
            foreach (var exe in launchModel.Executables)
            {
                this.logger.Log($"Adding kill command for executable {exe}");
                batchBuilder.Add(GetExecutableShutDownCommand(exe));
            }

            var folder = Path.GetDirectoryName(launchModel.ExecutableToLaunch);
            var executableName = Path.GetFileName(launchModel.ExecutableToLaunch);

            if (string.IsNullOrWhiteSpace(folder))
            {
                this.logger.Log($"Looks like you didn't specify a folder. No worries, I'll try to start {executableName}.");
            }
            else
            {
                batchBuilder.Add(GetFolderCDCommand(folder));
            }

            batchBuilder.Add(GetExecutableExecutionCommand(executableName, launchModel.Priority));

            batchBuilder.Echo($"Running {launchModel.ExecutableToLaunch}.");

            // Reboot explorer if it was shut down.
            if (launchModel.ShutdownExplorer)
            {
                batchBuilder.Echo($"Explorer.exe has been shut down.");
                batchBuilder.Echo($"Press any key to restart it.");
                batchBuilder.Echo($"Note that you really want to do this once you're done with {launchModel.ExecutableToLaunch}");
                batchBuilder.Pause();
                batchBuilder.Add(Domain.Constants.KnownProcesses.ExplorerExe);
            }

            var batchFileContent = batchBuilder.ToString();
            var batchRunner = this.batchRunnerFactory.Create(batchFileContent);

            var process = batchRunner.Run();

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
            return $"start \"{executableName}\" /{priorityClass.ToString().ToUpper()} \"{executableName}\"";
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
