// <copyright file="MonitoringEventModel.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Domain.Models
{
    /// <summary>
    /// Result model for the monitoring service.
    /// </summary>
    public class MonitoringEventModel
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the type of the process.
        /// </summary>
        /// <value>
        /// The type of the process.
        /// </value>
        public ProcessType ProcessType { get; set; }
    }
}