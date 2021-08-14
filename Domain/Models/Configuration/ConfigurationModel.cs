// <copyright file="ConfigurationModel.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Domain.Models.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;

    /// <summary>
    /// Configuration.
    /// </summary>
    public class ConfigurationModel
    {
        private string[]? executables;
        private string[]? services;

        /// <summary>
        /// Gets or sets a value indicating whether [shutdown explorer].
        /// </summary>
        [Description("When true the first thing I will do is shut down explorer. This prevents explorer triggering restarts of services and executables. Default is true.")]
        public bool ShutdownExplorer { get; set; } = true;

        /// <summary>
        /// Gets or sets the process priority class.
        /// </summary>
        [Description("The process priority I will use to run your program. Valid values are: Normal (default), Idle, High, RealTime, BelowNormal, AboveNormal. Default is 'AboveNormal'.")]
        public string Priority { get; set; } = ProcessPriorityClass.AboveNormal.ToString();

        /// <summary>
        /// Gets or sets the preferred editor.
        /// </summary>
        [Description("Here you can specify which editor you want to use to edit settings. If your editor can be found using a PATH, just specify the exe. Otherwise use the full path. Default is notepad.exe")]
        public string PreferredEditor { get; set; } = "notepad.exe";

        /// <summary>
        /// Gets or sets the monitoring interval.
        /// </summary>
        [Description("Interval, in milliseconds, how often I check if something started while you're running you're program.")]
        public int MonitoringInterval { get; set; } = 30000;

        /// <summary>
        /// Gets or sets the services.
        /// </summary>
        [Description("List services you wish to shut down by their full name in this section. For example: - My Service")]
        public string[] Services { get => this.services ?? Array.Empty<string>(); set => this.services = value; }

        /// <summary>
        /// Gets or sets the executables.
        /// </summary>
        [Description("List executables you wish to shut down. For example: - MyExecutable")]
        public string[] Executables { get => this.executables ?? Array.Empty<string>(); set => this.executables = value; }
    }
}
