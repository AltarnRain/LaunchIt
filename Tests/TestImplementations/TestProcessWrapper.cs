// <copyright file="TestProcessWrapper.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Tests.TestImplementations
{
    using Infrastructure.Helpers;
    using System.Collections.Generic;
    using System.Diagnostics;

    /// <summary>
    /// Test implementation for the process wrapper.
    /// </summary>
    /// <seealso cref="Infrastructure.Helpers.IProcessWrapper" />
    public class TestProcessWrapper : IProcessWrapper
    {
        /// <summary>
        /// Gets or sets the kill calls.
        /// </summary>
        public int KillCalls { get; set; }

        /// <summary>
        /// Gets or sets the start calls.
        /// </summary>
        public int StartCalls { get; set; }

        /// <summary>
        /// Kills the specified process.
        /// </summary>
        /// <param name="process">The process.</param>
        /// <returns>
        /// Killed processes.
        /// </returns>
        public IEnumerable<Process?> Kill(string process)
        {
            this.KillCalls++;
            yield return null;
        }

        /// <summary>
        /// Starts the specified executable.
        /// </summary>
        /// <param name="executable">The executable.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>
        /// Started process.
        /// </returns>
        public Process? Start(string executable, string? arguments = null)
        {
            this.StartCalls++;
            return null;
        }
    }
}
