﻿// <copyright file="ConfigurationModel.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Domain.Models.Configuration
{
    using System;

    /// <summary>
    /// Configuration.
    /// </summary>
    public class ConfigurationModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether [monitor restarts].
        /// </summary>
        public bool MonitorRestarts { get; set; }

        /// <summary>
        /// Gets or sets the services.
        /// </summary>
        public string[] Services { get; set; } = Array.Empty<string>();

        /// <summary>
        /// Gets or sets the executables.
        /// </summary>
        public string[] Executables { get; set; } = Array.Empty<string>();
    }
}
