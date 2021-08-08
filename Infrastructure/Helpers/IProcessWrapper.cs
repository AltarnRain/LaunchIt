// <copyright file="IProcessWrapper.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Helpers
{
    using System.Diagnostics;

    /// <summary>
    /// Contract for the process wrapper. Needed to test classes that start processes.
    /// </summary>
    public interface IProcessWrapper
    {
        /// <summary>
        /// Kills the specified process, yield processes because a single name can hide multiple processes.
        /// </summary>
        /// <param name="executable">The executable.</param>
        void Kill(string executable);

        /// <summary>
        /// Starts the specified executable.
        /// </summary>
        /// <param name="executable">The executable.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>Started process.</returns>
        Process? Start(string executable, string? arguments = null);
    }
}