// <copyright file="TestServiceHelper.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Tests.TestImplementations
{
    using Logic.Contracts.Helpers;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Test implementation of <see cref="IServiceHelper"/>.
    /// </summary>
    /// <seealso cref="Logic.Contracts.Helpers.IServiceHelper" />
    public class TestServiceHelper : IServiceHelper
    {
        /// <summary>
        /// Gets the services.
        /// </summary>
        public string[] Services { get; private set; } = Array.Empty<string>();

        /// <summary>
        /// Gets or sets the stop calls.
        /// </summary>
        public List<string> StopCalls { get; set; } = new();

        /// <summary>
        /// Gets the running services.
        /// </summary>
        /// <returns>
        /// List is service names that have status 'Running'.
        /// </returns>
        public string[] GetRunningServices()
        {
            return this.Services;
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
