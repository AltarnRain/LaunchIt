// <copyright file="Startup.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Presentation
{
    using Domain.Models.Configuration;
    using Infrastructure.Helpers;
    using Infrastructure.Loggers;
    using Infrastructure.Parsers;
    using Infrastructure.Providers;
    using Logic;
    using Logic.Contracts.Helpers;
    using Logic.Contracts.Providers;
    using Logic.Contracts.Services;
    using System.Runtime.Versioning;
    using System.Security.Principal;

    /// <summary>
    /// Starts up the application.
    /// </summary>
    [SupportedOSPlatform("windows")]
    public class Startup
    {
        private readonly LaunchIt launchIt;
        private readonly ILogEventService logger;
        private readonly IConfigurationService configurationService;
        private readonly IProcessHelper processHelper;
        private readonly LaunchModelProvider launchModelProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup" /> class.
        /// </summary>
        /// <param name="main">The main.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="configurationService">The configuration service.</param>
        /// <param name="processHelper">The process helper.</param>
        /// <param name="launchModelProvider">The launch model provider.</param>
        public Startup(
            LaunchIt main,
            ILogEventService logger,
            IConfigurationService configurationService,
            IProcessHelper processHelper,
            LaunchModelProvider launchModelProvider)
        {
            this.launchIt = main;
            this.logger = logger;
            this.configurationService = configurationService;
            this.processHelper = processHelper;
            this.launchModelProvider = launchModelProvider;
        }

        /// <summary>
        /// Runs the specified argument.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public void Run(string[] args)
        {
            var launchModel = this.launchModelProvider.GetModel(args);

            if (launchModel.ResetConfiguration)
            {
                this.configurationService.WriteExampleConfigurationFile();
                return;
            }

            if (launchModel.EditConfiguration)
            {
                this.configurationService.EditInNotepad();
                return;
            }

            if (!IsElevated())
            {
                this.logger.Log("Hey, I noticed you're not running me as administrator.");
                this.logger.Log("I'll still do my best to shutdown processes for you but without administrative priveledges there's only so much I can do.");
            }

            // Create loggers we want to register.
            var consoleLogger = new ConsoleLogger();
            var fileLogger = new FileLogger();

            // Subscribe loggers. They'll be informed of any log message and deal with it.
            var fileLoggerSub = this.logger.Subscribe(fileLogger);
            var consoleLoggerSub = this.logger.Subscribe(consoleLogger);

            this.launchIt.Start(launchModel);

            // Close the file logger. This writes cached logs into the log file.
            fileLogger.Close();

            var logFile = fileLogger.FileName;
            this.processHelper.Start(logFile);

            fileLoggerSub();
            consoleLoggerSub();

            this.WaitForUserShutDownCommand();
        }

        private static bool IsElevated()
        {
            var owner = WindowsIdentity.GetCurrent().Owner;

            if (owner is null)
            {
                return false;
            }

            return owner.IsWellKnown(WellKnownSidType.BuiltinAdministratorsSid);
        }

        private void WaitForUserShutDownCommand()
        {
            var manualResetEvent = new System.Threading.ManualResetEvent(false);
            this.logger.Log("Click 'X' to close the program.");
            this.logger.Log("Or, press CTRL-C to return to the command prompt.");
            manualResetEvent.WaitOne();
        }
    }
}
