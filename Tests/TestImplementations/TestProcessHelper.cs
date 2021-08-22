// <copyright file="TestProcessHelper.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Tests.TestImplementations
{
    using Logic.Contracts.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    /// <summary>
    /// Test implementation of <see cref="IProcessHelper" />.
    /// </summary>
    /// <seealso cref="Logic.Contracts.Helpers.IProcessHelper" />
    public class TestProcessHelper : IProcessHelper
    {
        /// <summary>
        /// Gets or sets the executables.
        /// </summary>
        public Func<string[]>? HandleGetRunningExecutables { get; set; }

        /// <summary>
        /// Gets or sets the start calls.
        /// </summary>
        public List<string> StartCalls { get; set; } = new();

        /// <summary>
        /// Gets or sets the stop calls.
        /// </summary>
        public List<string> StopCalls { get; set; } = new();

        /// <summary>
        /// Gets the running processes.
        /// </summary>
        /// <returns>
        /// Running processes.
        /// </returns>
        public string[] GetRunningExecutables()
        {
            if (this.HandleGetRunningExecutables is null)
            {
                return Array.Empty<string>();
            }

            return this.HandleGetRunningExecutables();
        }

        /// <summary>
        /// Starts something.
        /// </summary>
        /// <param name="name">Name of what you want to start.</param>
        /// <returns>A process.</returns>
        public Process? Start(string name)
        {
            this.StartCalls.Add(name);
            return null;
        }

        /// <summary>
        /// Stops the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        public void Stop(string name)
        {
            this.StopCalls.Add(name);
        }
    }
}
