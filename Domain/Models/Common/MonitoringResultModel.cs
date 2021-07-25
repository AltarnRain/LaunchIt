// <copyright file="MonitoringResultModel.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Domain.Models.Common
{
    using System;

    /// <summary>
    /// Model for a monitoring result.
    /// </summary>
    public class MonitoringResultModel
    {
        /// <summary>
        /// Gets or sets the started processes.
        /// </summary>
        public string[] StartedProcesses { get; set; } = Array.Empty<string>();

        /// <summary>
        /// Gets or sets the started serices.
        /// </summary>
        public string[] StartedServices { get; set; } = Array.Empty<string>();
    }
}
