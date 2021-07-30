// <copyright file="CommandLineParserResult.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Parsers
{
    using Domain.Models.Configuration;

    /// <summary>
    /// Result of a command line parse.
    /// </summary>
    public class CommandLineParserResult
    {
        /// <summary>
        /// Gets or sets a value indicating whether [reset configuration].
        /// </summary>
        public bool ResetConfiguration { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [edit configuration].
        /// </summary>
        public bool EditConfiguration { get; set; }

        /// <summary>
        /// Gets or sets the launch model.
        /// </summary>
        public LaunchModel LaunchModel { get; set; } = new();
    }
}