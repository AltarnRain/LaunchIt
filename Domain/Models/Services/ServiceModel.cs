// <copyright file="ServiceModel.cs" company="Onno Invernizzi">
// Copyright (c) Onno Invernizzi. All rights reserved.
// </copyright>

namespace Domain.Models.Services
{
    /// <summary>
    /// Model for a service.
    /// </summary>
    public class ServiceModel
    {
        /// <summary>
        /// Gets or sets the name of the service.
        /// </summary>
        /// <value>
        /// The name of the service.
        /// </value>
        public string ServiceName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ServiceModel"/> is running.
        /// </summary>
        public bool Running { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ServiceModel"/> is enabled.
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
    }
}
