// <copyright file="ServiceShutdownConfiguration.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Domain.Models.Configuration
{
    using System.ComponentModel;

    /// <summary>
    /// Model for ServiceShutdown configuration.
    /// </summary>
    public class ServiceShutdownConfiguration
    {
        /// <summary>
        /// Gets or sets a value indicating whether [shutdown restarted services].
        /// </summary>
        [Description("When LaunchIt detects a restarted service it will attempt to stop it.")]
        public bool ShutdownRestartedServices { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether [only shutdown configured services].
        /// </summary>
        [Description("When true LaunchIt will only stop a service you've configured to shut down.")]
        public bool OnlyShutdownConfiguredServices { get; set; } = true;
    }
}
