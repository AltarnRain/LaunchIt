// <copyright file="CommandTypes.cs" company="Onno Invernizzi">
// Copyright (c) Onno Invernizzi. All rights reserved.
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
