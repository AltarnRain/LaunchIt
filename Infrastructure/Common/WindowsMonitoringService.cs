// <copyright file="WindowsMonitoringService.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Common
{
    using Domain.Models.Common;
    using Logic.Common;
    using System;

    /// <summary>
    /// Monitors servcices and executables that were started.
    /// </summary>
    /// <seealso cref="Logic.Common.IMonitoringService" />
    public class WindowsMonitoringService : IMonitoringService
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsMonitoringService"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public WindowsMonitoringService(ILogger logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:Logic.Common.IMonitoringService" /> is monitoring.
        /// </summary>
        public bool Monitoring { get; private set; }

        /// <summary>
        /// Ends the monitoring.
        /// </summary>
        /// <returns>
        /// A list of names of things that were restarted while monitoring.
        /// </returns>
        public MonitoringModel[] EndMonitoring()
        {
            if (this.Monitoring == false)
            {
                throw new Exception("Did not begin moniroging");
            }

            return Array.Empty<MonitoringModel>();
        }

        /// <summary>
        /// Starts the monitoring.
        /// </summary>
        /// <exception cref="Exception">Already monitoring.</exception>
        public void StartMonitoring()
        {
            if (this.Monitoring)
            {
                throw new Exception("Already monitoring");
            }
        }
    }
}
