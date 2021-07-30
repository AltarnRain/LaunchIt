// <copyright file="ShutdownConfigurationModel.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Domain.Models.Configuration
{
    using System.ComponentModel;

    /// <summary>
    /// Model for ServiceShutdown configuration.
    /// </summary>
    public class ShutdownConfigurationModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether [shutdown restarted services].
        /// </summary>
        [Description("Performs a shutdown when I detect a (re)start. Default is true")]
        public bool ShutdownAfterRestart { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether [only shutdown configured services].
        /// </summary>
        [Description("Limit shut down commands to what you configured you wanted to shutdown. Default is true")]
        public bool OnlyConfigured { get; set; } = true;

        /// <summary>
        /// Gets or sets the maximum shutdown attempts.
        /// </summary>
        [Description("Configures the maximum amount that I will attempt to do a shutdown. Default is 3. Set to -1 for infinite attempts.")]
        public int MaximumShutdownAttempts { get; set; } = 3;
    }
}
