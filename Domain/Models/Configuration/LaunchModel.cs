// <copyright file="LaunchModel.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Domain.Models.Configuration
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Model for launching an executable.
    /// </summary>
    public class LaunchModel
    {
        /// <summary>
        /// Gets or sets the executable to launch.
        /// </summary>
        public string ExecutableToLaunch { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the services.
        /// </summary>
        public string[] Services { get; set; } = Array.Empty<string>();

        /// <summary>
        /// Gets or sets the executables.
        /// </summary>
        public string[] Executables { get; set; } = Array.Empty<string>();

        /// <summary>
        /// Gets or sets a value indicating whether [shutdown explorer].
        /// </summary>
        public bool ShutdownExplorer { get; set; } = true;

        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        public ProcessPriorityClass Priority { get; set; } = ProcessPriorityClass.AboveNormal;

        /// <summary>
        /// Gets or sets a value indicating whether [use batch file].
        /// </summary>
        public bool UseBatchFile { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [edit configuration].
        /// </summary>
        public bool EditConfiguration { get; set; }

        /// <summary>
        /// Gets or sets the monitoring interval.
        /// </summary>
        public int MonitoringInterval { get; set; }
    }
}
