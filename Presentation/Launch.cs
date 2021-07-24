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
        /// <summary>
        /// The main.
        /// </summary>
        private readonly GameLauncher gameLauncher;

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// The configuration service.
        /// </summary>
        private readonly IConfigurationService configurationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="Launch" /> class.
        /// </summary>
        /// <param name="main">The main.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="configurationService">The configuration service.</param>
        public Launch(
            GameLauncher main,
            ILogger logger,
            IConfigurationService configurationService)
        {
            this.gameLauncher = main;
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
                default:
                    this.gameLauncher.Start(argument);
                    break;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is elevated.
        /// </summary>
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
