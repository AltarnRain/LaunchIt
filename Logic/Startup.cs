// <copyright file="Startup.cs" company="Onno Invernizzi">
// Copyright (c) Onno Invernizzi. All rights reserved.
// </copyright>

namespace Logic
{
    using Logic.Common;

    /// <summary>
    /// Startup class for the program.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// The window service.
        /// </summary>
        private readonly IWindowServices windowServices;

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="windowServicesProvider">The window services provider.</param>
        /// <param name="logger">The logger.</param>
        public Startup(IWindowServices windowServicesProvider, ILogger logger)
        {
            this.windowServices = windowServicesProvider;
            this.logger = logger;
        }

        /// <summary>
        /// Starts the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public void Start(string[] args)
        {
        }
    }
}
