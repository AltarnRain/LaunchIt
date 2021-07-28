﻿// <copyright file="Launch.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Presentation
{
    using Infrastructure.Helpers;
    using Infrastructure.Loggers;
    using Logic;
    using Logic.Helpers;
    using Logic.Services;
    using System.Runtime.Versioning;
    using System.Security.Principal;

    /// <summary>
    /// Launches the application.
    /// </summary>
    [SupportedOSPlatform("windows")]
    public class Launch
    {
        private readonly LaunchIt launchIt;
        private readonly ILogEventService logger;
        private readonly IConfigurationService configurationService;
        private readonly IProcessHelper processHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="Launch" /> class.
        /// </summary>
        /// <param name="main">The main.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="configurationService">The configuration service.</param>
        /// <param name="processHelper">The process helper.</param>
        public Launch(
            LaunchIt main,
            ILogEventService logger,
            IConfigurationService configurationService,
            IProcessHelper processHelper)
        {
            this.launchIt = main;
            this.logger = logger;
            this.configurationService = configurationService;
            this.processHelper = processHelper;
        }

        /// <summary>
        /// Runs the specified argument.
        /// </summary>
        /// <param name="argument">The argument.</param>
        public void Run(string argument)
        {
            switch (argument.ToLower())
            {
                case "edit":
                    this.configurationService.EditInNotepad();
                    break;
                case "reset":
                    this.configurationService.WriteExampleConfigurationFile();
                    this.configurationService.EditInNotepad();
                    break;
                default:
                    if (!IsElevated())
                    {
                        this.logger.Log("Hey, I noticed you're not running me as administrator.");
                        this.logger.Log("I'll still do my best to shutdown processes for you but without administrative priveledges there's only so much I can do.");
                    }

                    var consoleLogger = new ConsoleLogger();
                    var fileLogger = new FileLogger();

                    var fileLoggerSub = this.logger.Subscribe(fileLogger.Log);
                    var consoleLoggerSub = this.logger.Subscribe(consoleLogger.Log);

                    this.launchIt.Start(argument);

                    // Close the file logger. This writes cached logs into the log file.
                    fileLogger.Close();

                    var logFile = fileLogger.FileName;
                    this.processHelper.Start(logFile);

                    fileLoggerSub();
                    consoleLoggerSub();

                    this.WaitForUserShutDownCommand();

                    break;
            }
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
