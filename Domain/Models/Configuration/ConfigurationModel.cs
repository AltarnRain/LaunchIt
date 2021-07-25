// <copyright file="ConfigurationModel.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Domain.Models.Configuration
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;

    /// <summary>
    /// Configuration.
    /// </summary>
    public class ConfigurationModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether [monitor restarts].
        /// </summary>
        [Description("Set this to true if you want LaunchIt to monitor executables and services that were (re)started.")]
        public bool MonitorRestarts { get; set; }

        /// <summary>
        /// Gets or sets the monitoring interval.
        /// </summary>
        [Description("Specifies the time in milliseconds LaunchIt will wait before checking for services or processes that were started.")]
        public int MonitoringInterval { get; set; } = 5000;

        /// <summary>
        /// Gets or sets a value indicating whether [shutdown explorer].
        /// </summary>
        [Description("When true the first thing LaunchIt will do is shut down explorer. This prevents explorer triggering restarts of services and executables.")]
        public bool ShutdownExplorer { get; set; }

        /// <summary>
        /// Gets or sets the process priority class.
        /// </summary>
        [Description("The process priority LauncIt will use to run your program. Valid values are: Normal (default), Idle, High, RealTime, BelowNormal, AboveNormal.")]
        public string Priority { get; set; } = ProcessPriorityClass.Normal.ToString();

        /// <summary>
        /// Gets or sets the preferred editor.
        /// </summary>
        [Description("Here you can specify which editor you want to use to edit settings. If your editor can be found using a PATH, just specify the exe. Otherwise use the full path.")]
        public string PreferredEditor { get; set; } = "notepad.exe";

        /// <summary>
        /// Gets or sets the services.
        /// </summary>
        [Description("List services you wish to shut down by their full name in this section. For example: - My Service")]
        public string[] Services { get; set; } = Array.Empty<string>();

        /// <summary>
        /// Gets or sets the executables.
        /// </summary>
        [Description("List executables you wish to shut down. For example: - MyExecutable")]
        public string[] Executables { get; set; } = Array.Empty<string>();
    }
}
