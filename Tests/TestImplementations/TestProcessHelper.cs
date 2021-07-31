// <copyright file="TestProcessHelper.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Tests.TestImplementations
{
    using Logic.Contracts.Helpers;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Test implementation of <see cref="IProcessHelper"/>.
    /// </summary>
    /// <seealso cref="Logic.Contracts.Helpers.IProcessHelper" />
    public class TestProcessHelper : IProcessHelper
    {
        /// <summary>
        /// Gets or sets the executables.
        /// </summary>
        public string[] Executables { get; set; } = Array.Empty<string>();

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
            return this.Executables;
        }

        /// <summary>
        /// Stops the count.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>
        /// Number of times 'name' has been stopped.
        /// </returns>
        public int GetStopCount(string name)
        {
            return 0;
        }

        /// <summary>
        /// Starts something.
        /// </summary>
        /// <param name="name">Name of what you want to start.</param>
        public void Start(string name)
        {
            this.StartCalls.Add(name);
        }

        /// <summary>
        /// Stops the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="tackCount">if set to <c>true</c> [tack count].</param>
        public void Stop(string name, bool tackCount = false)
        {
            this.StopCalls.Add(name);
        }
    }
}
