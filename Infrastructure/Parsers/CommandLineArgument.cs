// <copyright file="CommandLineArgument.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Domain.Models.Configuration
{
    using System;

    /// <summary>
    /// Model for a command line arguments.
    /// </summary>
    public class CommandLineArgument
    {
        /// <summary>
        /// Gets or sets the command.
        /// </summary>
        public string Command { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the options.
        /// </summary>
        public string[] Options { get; set; } = Array.Empty<string>();
    }
}
