// <copyright file="MonitoringConfiguration.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Domain.Models.Configuration
{
    using System.ComponentModel;

    /// <summary>
    /// Model for monitoring configurations.
    /// </summary>
    public class MonitoringConfiguration
    {
        /// <summary>
        /// Gets or sets a value indicating whether [monitor restarts].
        /// </summary>
        [Description("Set this to true if you want me to monitor executables and services that were (re)started.")]
        public bool MonitorRestarts { get; set; } = true;

        /// <summary>
        /// Gets or sets the monitoring interval.
        /// </summary>
        [Description("Specifies the time in milliseconds I will wait before checking for services or processes that were started.")]
        public int MonitoringInterval { get; set; } = 5000;
    }
}
