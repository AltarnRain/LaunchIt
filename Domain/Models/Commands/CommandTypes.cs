// <copyright file="CommandTypes.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Domain.Models.Commands
{
    /// <summary>
    /// Type of commands passed via command line arguments.
    /// </summary>
    public enum CommandType
    {
        /// <summary>
        /// Unknown command
        /// </summary>
        Unknown,

        /// <summary>
        /// The service list
        /// </summary>
        ServiceList,
    }
}
