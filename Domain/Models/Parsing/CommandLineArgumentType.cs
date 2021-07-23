// <copyright file="CommandLineArgumentType.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Domain.Models.Parsing
{
    /// <summary>
    /// Types of arguments that can be passed via the command line.
    /// </summary>
    public enum CommandLineArgumentType
    {
        /// <summary>
        /// The unknown
        /// </summary>
        Unknown,

        /// <summary>
        /// The initialize
        /// </summary>
        Initialize,

        /// <summary>
        /// The no explorer
        /// </summary>
        NoExplorer,

        /// <summary>
        /// The run game
        /// </summary>
        RunGame,

        /// <summary>
        /// The priority
        /// </summary>
        Priority,
    }
}
