// <copyright file="ServiceInfo.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Domain.Models.Programs
{
    /// <summary>
    /// Information about a service.
    /// </summary>
    public class ServiceInfo
    {
        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether this service is enabled.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance can stop.
        /// </summary>
        public bool CanStop { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance can pause and continue.
        /// </summary>
        public bool CanPauseAndContinue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance can shutdown.
        /// </summary>
        public bool CanShutdown { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this service is running.
        /// </summary>
        public bool Running { get; set; }
    }
}
