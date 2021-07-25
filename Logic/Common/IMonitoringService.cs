// <copyright file="IMonitoringService.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Logic.Common
{
    using Domain.Models.Common;

    /// <summary>
    /// Contracts for a class that monitors restarts of services and executables.
    /// </summary>
    public interface IMonitoringService
    {
        /// <summary>
        /// Gets a value indicating whether this <see cref="IMonitoringService"/> is monitoring.
        /// </summary>
        bool Monitoring { get; }

        /// <summary>
        /// Starts the monitoring.
        /// </summary>
        void StartMonitoring();

        /// <summary>
        /// Ends the monitoring.
        /// </summary>
        /// <returns>A list of names of things that were restarted while monitoring.</returns>
        MonitoringResultModel EndMonitoring();
    }
}
