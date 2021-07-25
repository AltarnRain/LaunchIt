// <copyright file="Launch.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Presentation
{
    using Logic;
    using Logic.Common;
    using System.Runtime.Versioning;
    using System.Security.Principal;

    /// <summary>
    /// Launches the application.
    /// </summary>
    [SupportedOSPlatform("windows")]
    public class Launch
    {
        private readonly LaunchIt launchIt;
        private readonly ILogger logger;
        private readonly IConfigurationService configurationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="Launch" /> class.
        /// </summary>
        /// <param name="main">The main.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="configurationService">The configuration service.</param>
        public Launch(
            LaunchIt main,
            ILogger logger,
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
            if (!IsElevated())
            {
                this.logger.Log("This program requires Administrative access");
                return;
            }

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
