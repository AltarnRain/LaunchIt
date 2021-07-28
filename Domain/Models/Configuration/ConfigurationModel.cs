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
    public class ConfigurationModel : VersionedModel
    {
        private string[]? executables;
        private string[]? services;

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        [Description("Version of the configuration file. Do not change this!")]
        public override int Version { get; set; } = 1;

        /// <summary>
        /// Gets or sets a value indicating whether [shutdown explorer].
        /// </summary>
        [Description("When true the first thing LaunchIt will do is shut down explorer. This prevents explorer triggering restarts of services and executables. Default is true.")]
        public bool ShutdownExplorer { get; set; } = true;

        /// <summary>
        /// Gets or sets the process priority class.
        /// </summary>
        [Description("The process priority LaunchIt will use to run your program. Valid values are: Normal (default), Idle, High, RealTime, BelowNormal, AboveNormal. Default is 'AboveNormal'.")]
        public string Priority { get; set; } = ProcessPriorityClass.AboveNormal.ToString();

        /// <summary>
        /// Gets or sets the preferred editor.
        /// </summary>
        [Description("Here you can specify which editor you want to use to edit settings. If your editor can be found using a PATH, just specify the exe. Otherwise use the full path. Default is notepad.exe")]
        public string PreferredEditor { get; set; } = "notepad.exe";

        /// <summary>
        /// Gets or sets a value indicating whether [use batch file].
        /// </summary>
        [Description("LaunchIt will make a batch file to launch the program you specified and then shut down itself. Default is false.")]
        public bool UseBatchFile { get; set; } = false;

        /// <summary>
        /// Gets or sets the monitoring configuration.
        /// </summary>
        [Description("Configure monitoring options")]
        public MonitoringConfiguration MonitoringConfiguration { get; set; } = new();

        /// <summary>
        /// Gets or sets the service shutdown configuration.
        /// </summary>
        [Description("Configure shutting down services that (re)start.")]
        public ShutdownConfigurationModel ServiceShutdownConfiguration { get; set; } = new();

        /// <summary>
        /// Gets or sets the executable shutdown configuration.
        /// </summary>
        [Description("Configure shutting down executables that (re)start.")]
        public ShutdownConfigurationModel ExecutableShutdownConfiguration { get; set; } = new();

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
