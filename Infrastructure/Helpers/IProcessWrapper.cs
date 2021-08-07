// <copyright file="IProcessWrapper.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Helpers
{
    using System.Collections.Generic;
    using System.Diagnostics;

    /// <summary>
    /// Contract for the process wrapper. Needed to test classes that start processes.
    /// </summary>
    public interface IProcessWrapper
    {
        /// <summary>
        /// Kills the specified process.
        /// </summary>
        /// <param name="process">The process.</param>
        /// <returns>Killed processes.</returns>
        IEnumerable<Process?> Kill(string process);

        /// <summary>
        /// Starts the specified executable.
        /// </summary>
        /// <param name="executable">The executable.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>Started process.</returns>
        Process? Start(string executable, string? arguments = null);
    }
}