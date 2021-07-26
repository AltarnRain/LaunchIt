// <copyright file="Launch.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Presentation
{
    using Logic;
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
        private readonly ILoggerService logger;
        private readonly IConfigurationService configurationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="Launch" /> class.
        /// </summary>
        /// <param name="main">The main.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="configurationService">The configuration service.</param>
        public Launch(
            LaunchIt main,
            ILoggerService logger,
            IConfigurationService configurationService)
        {
            this.launchIt = main;
            this.logger = logger;
            this.configurationService = configurationService;
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

                    this.launchIt.Start(argument);
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
    }
}
