// <copyright file="Startup.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Logic
{
    using Logic.Common;
    using Logic.Providers;

    /// <summary>
    /// Startup class for the program.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// The window service.
        /// </summary>
        private readonly IRunningProgramsHelper windowServices;

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger logger;

        private readonly IPathProvider pathProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup" /> class.
        /// </summary>
        /// <param name="windowServicesProvider">The window services provider.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="pathProvider">The path provider.</param>
        public Startup(IRunningProgramsHelper windowServicesProvider, ILogger logger, IPathProvider pathProvider)
        {
            this.windowServices = windowServicesProvider;
            this.logger = logger;
            this.pathProvider = pathProvider;
        }

        /// <summary>
        /// Starts the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public void Start(string[] args)
        {
            this.logger.Log("I ran");
            var p = this.pathProvider.MapPath("~/");

            this.logger.Log(p);
        }
    }
}
