// <copyright file="GameLauncher.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Logic
{
    using Logic.Common;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// Startup class for the program.
    /// </summary>
    public class GameLauncher
    {
        /// <summary>
        /// The configuration service.
        /// </summary>
        private readonly IConfigurationService configurationService;

        /// <summary>
        /// The program helper.
        /// </summary>
        private readonly IProgramHelper programHelper;

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameLauncher" /> class.
        /// </summary>
        /// <param name="configurationService">The configuration service.</param>
        /// <param name="programHelper">The program helper.</param>
        /// <param name="logger">The logger.</param>
        public GameLauncher(
            IConfigurationService configurationService,
            IProgramHelper programHelper,
            ILogger logger)
        {
            this.configurationService = configurationService;
            this.programHelper = programHelper;
            this.logger = logger;
        }

        /// <summary>
        /// Starts the specified arguments.
        /// </summary>
        /// <param name="gameExecutable">The game executable.</param>
        public void Start(string? gameExecutable)
        {
            var didWork = this.CheckForConfigurationFile();

            if (didWork)
            {
                this.configurationService.EditInNotepad();
                return;
            }

            if (gameExecutable is null)
            {
                this.logger.Log($"You didn't give me a game to start so I'll just shutdown services and executables.");
            }
            else
            {
                if (!File.Exists(gameExecutable))
                {
                    this.logger.Log($"I couldn't find a file at {gameExecutable} so I'm calling it quits. Make sure to specify the full path and executable.");
                    return;
                }
                else
                {
                    this.logger.Log($"Getting things ready to launch '{gameExecutable}'");
                }
            }

            var configuration = this.configurationService.Read();

            if (configuration.Services.Length + configuration.Executables.Length == 0)
            {
                this.logger.Log("You didn't configure and services or executables for me to shut down. You're probably better off just launching the game");
                return;
            }

            var tasks = new List<Task>();

            this.logger.Log("Stopping services...");

            foreach (var service in configuration.Services)
            {
                var stopTask = this.programHelper.Stop(service);
                tasks.AddRange(stopTask);
            }

            // Empty line for readability.
            this.logger.Log(string.Empty);

            this.logger.Log("Stopping executables...");

            foreach (var executable in configuration.Executables)
            {
                var stopTask = this.programHelper.Stop(executable);
                tasks.AddRange(stopTask);
            }

            // Wait for all tasks to complete.
            this.logger.Log("Waiting for services and executables to shutdown");
            Task.WaitAll(tasks.ToArray());

            if (gameExecutable is null)
            {
                return;
            }

            var processStartInfo = new ProcessStartInfo
            {
                UseShellExecute = true,
                FileName = gameExecutable,
            };

            var process = Process.Start(processStartInfo);

            if (process is null)
            {
                this.logger.Log($"Something went wrong... sorry I can't launch the game.");
                return;
            }

            process.WaitForExit();
        }

        private bool CheckForConfigurationFile()
        {
            if (!this.configurationService.ConfigurationFileExists())
            {
                this.configurationService.WriteExampleConfigurationFile();
                this.logger.Log($"Looks like you're starting GameLauncher for the first time. I'll setup an example configuration file and open it in notepad.");
                this.logger.Log($"If you want to to edit your configuration file just run GameLauncher with the 'edit' argument. For example:");
                this.logger.Log($"   GameLauncher edit");
                return true;
            }

            return false;
        }
    }
}
