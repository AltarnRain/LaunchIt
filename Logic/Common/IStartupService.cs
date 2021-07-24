// <copyright file="IStartupService.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Logic.Common
{
    using System.Diagnostics;

    /// <summary>
    /// Contract for a Startup Service.
    /// </summary>
    public interface IStartupService
    {
        /// <summary>
        /// Starts the specified executable.
        /// </summary>
        /// <param name="executable">The executable.</param>
        /// <param name="priorityClass">The priority class.</param>
        /// <returns>
        /// A Process.
        /// </returns>
        public Process Start(string? executable, ProcessPriorityClass priorityClass);
    }
}
