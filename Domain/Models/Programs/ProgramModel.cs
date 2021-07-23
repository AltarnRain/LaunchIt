// <copyright file="ProgramModel.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Domain.Models.Programs
{
    /// <summary>
    /// Model for a service.
    /// </summary>
    public class ProgramModel
    {
        /// <summary>
        /// Gets or sets the name of the service.
        /// </summary>
        /// <value>
        /// The name of the service.
        /// </value>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ProgramModel"/> is running.
        /// </summary>
        public bool Running { get; set; }

        /// <summary>
        /// Gets or sets the service information.
        /// </summary>
        public ServiceInfo? ServiceInfo { get; set; }

        /// <summary>
        /// Gets or sets the type of the program.
        /// </summary>
        /// <value>
        /// The type of the program.
        /// </value>
        public ProgramType ProgramType { get; set; }
    }
}