// <copyright file="CommandLineParseResult.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Domain.Models.Parsing
{
    using System;

    /// <summary>
    /// Result model parse of command line arguments.
    /// </summary>
    public class CommandLineParseResult
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CommandLineParseResult"/> is succes.
        /// </summary>
        public bool Succes { get; set; }

        /// <summary>
        /// Gets or sets the error messages.
        /// </summary>
        public string[] ErrorMessages { get; set; } = Array.Empty<string>();

        /// <summary>
        /// Gets or sets the command line argument infos.
        /// </summary>
        public CommandLineArgumentInfo[] CommandLineArgumentInfos { get; set; } = Array.Empty<CommandLineArgumentInfo>();
    }
}