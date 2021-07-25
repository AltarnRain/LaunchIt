// <copyright file="LaunchIt.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Logic
{
    using Logic.Common;
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Startup class for the program.
    /// </summary>
    public class LaunchIt
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
        /// The startup service.
        /// </summary>
        private readonly IStartupService startupService;

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchIt" /> class.
        /// </summary>
        /// <param name="configurationService">The configuration service.</param>
        /// <param name="programHelper">The program helper.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="startupService">The startup service.</param>
        public LaunchIt(
            IConfigurationService configurationService,
            ILogger logger,
            IStartupService startupService)
        {
            this.configurationService = configurationService;
            this.logger = logger;
            this.startupService = startupService;
        }

        /// <summary>
        /// Starts the specified arguments.
        /// </summary>
        /// <param name="executable">The executable.</param>
        public void Start(string? executable)
        {
            var didWork = this.CheckForConfigurationFile();

            if (didWork)
            {
                this.configurationService.EditInNotepad();
                return;
            }

            var configuration = this.configurationService.Read();

            if (configuration.Services.Length + configuration.Executables.Length == 0)
            {
                this.logger.Log("You didn't configure and services or executables for me to shut down.");
                return;
            }

            var enumValue = Enum.Parse<ProcessPriorityClass>(configuration.Priority, true);
            var process = this.startupService.Start(executable, enumValue);

            process.WaitForExit();
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
