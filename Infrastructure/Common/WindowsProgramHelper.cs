// <copyright file="WindowsProgramHelper.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Common
{
    using Logic.Common;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Runtime.Versioning;
    using System.ServiceProcess;
    using System.Threading.Tasks;

    /// <summary>
    /// Program helper for windows OS.
    /// </summary>
    /// <seealso cref="Logic.Common.IProgramHelper" />
    [SupportedOSPlatform("windows")]
    public class WindowsProgramHelper : IProgramHelper
    {
        /// <summary>
        /// The configuration service.
        /// </summary>
        private readonly IConfigurationService configurationService;

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsProgramHelper" /> class.
        /// </summary>
        /// <param name="configurationService">The configuration service.</param>
        /// <param name="logger">The logger.</param>
        public WindowsProgramHelper(IConfigurationService configurationService, ILogger logger)
        {
            this.configurationService = configurationService;
            this.logger = logger;
        }

        /// <summary>
        /// Stops a running process. Yields the task.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>
        /// A Task. Only yields a task if there's work to be done. Means there is no need to return a completed task if nothing has to be done.
        /// </returns>
        public IEnumerable<Task> Stop(string name)
        {
            // Get the configuration form the default configuration file.
            var configuration = this.configurationService.Read();

            // Check if we can find a service using the passed name.
            var isService = configuration.Services?.Any(s => s == name) ?? false;

            if (isService)
            {
                var foundService = ServiceController.GetServices().SingleOrDefault(s => s.DisplayName == name);

                if (foundService is null)
                {
                    this.logger.Log($"Could not find a service with name '{name}");
                    yield break;
                }

                if (foundService.Status == ServiceControllerStatus.Running)
                {
                    yield return this.StopAndLog(foundService.Stop, name, isService);
                    yield break;
                }

                this.logger.Log($"Service {name} is not running. Skipped.");
                yield break;
            }

            // Strip.exe
            var processName = name;
            if (name.ToLower().EndsWith(".exe"))
            {
                processName = Path.GetFileNameWithoutExtension(name);
            }

            var processes = Process.GetProcessesByName(processName);

            // No process found. We're done.
            if (processes is null || processes.Length == 0)
            {
                this.logger.Log($"No processes to stop with name '{processName}'. Skipped.");
                yield break;
            }

            // Shut down processes that match the name.
            foreach (var process in processes)
            {
                yield return this.StopAndLog(processes[0].Kill, processName, isService);
            }
        }

        /// <summary>
        /// Stops the and log.
        /// </summary>
        /// <param name="stopAction">The stop action.</param>
        /// <param name="name">The name.</param>
        /// <returns>A running task that stopping a service or killing an executable.</returns>
        private Task StopAndLog(Action stopAction, string name, bool isService)
        {
            return Task.Run(() =>
            {
                var processType = isService ? "service" : "executable";
                this.logger.Log($"Stopping {processType} '{name}'");
                stopAction();
            });
        }
    }
}
