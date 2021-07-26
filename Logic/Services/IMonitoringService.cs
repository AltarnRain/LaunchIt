// <copyright file="IMonitoringService.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Logic.Services
{
    using Domain.Models.Events;
    using System;

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
        void EndMonitoring();

        /// <summary>
        /// Subscribes the specified subscription.
        /// </summary>
        /// <param name="subscription">The subscription.</param>
        /// <returns>
        /// An action to remove the subscription.
        /// </returns>
        Action Subscribe(Action<MonitoringEventModel> subscription);
    }
}
