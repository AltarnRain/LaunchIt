// <copyright file="Startup.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Presentation
{
    using Infrastructure.Loggers;
    using Infrastructure.Providers;
    using Logic;
    using Logic.Contracts.Services;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Versioning;
    using System.Security.Principal;

    /// <summary>
    /// Starts up the application.
    /// </summary>
    [SupportedOSPlatform("windows")]
    [ExcludeFromCodeCoverage(Justification = "Starting point for DI. To big of integration test to cover this.")]
    public class Startup
    {
        private readonly LaunchIt launchIt;
        private readonly ILogEventService logger;
        private readonly LaunchModelProvider launchModelProvider;
        private readonly IEditorService editorService;

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup" /> class.
        /// </summary>
        /// <param name="main">The main.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="configurationService">The configuration service.</param>
        /// <param name="launchModelProvider">The launch model provider.</param>
        /// <param name="editorService">The editor service.</param>
        public Startup(
            LaunchIt main,
            ILogEventService logger,
            LaunchModelProvider launchModelProvider,
            IEditorService editorService)
        {
            this.launchIt = main;
            this.logger = logger;
            this.launchModelProvider = launchModelProvider;
            this.editorService = editorService;
        }

        /// <summary>
        /// Runs the specified argument.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public void Run(string[] args)
        {
            var launchModel = this.launchModelProvider.GetModel(args);

            if (launchModel.EditConfiguration)
            {
                this.editorService.EditConfiguration();
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

            try
            {
                this.launchIt.Start(launchModel);
            }
            catch
            {
                throw;
            }
            finally
            {
                // Close the file logger. This writes cached logs into the log file.
                fileLogger.Close();

                var logFile = fileLogger.FileName;

                this.logger.Log($"Log file written to: \"{logFile}\".");

                fileLoggerSub();

                this.WaitForUserShutDownCommand();

                consoleLoggerSub();
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
