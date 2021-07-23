// <copyright file="ICommandLineArgumentParser.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Logic.Common
{
    using Domain.Models.Parsing;

    /// <summary>
    /// Contract for a command line parser.
    /// </summary>
    public interface ICommandLineArgumentParser
    {
        /// <summary>
        /// Parses the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>A Tupple.</returns>
        public CommandLineParseResult Parse(string[] args);
    }
}
