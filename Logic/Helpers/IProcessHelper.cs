// <copyright file="IProcessHelper.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Logic.Helpers
{
    /// <summary>
    /// Contract for a process helper.
    /// </summary>
    public interface IProcessHelper
    {
        /// <summary>
        /// Gets the running processes.
        /// </summary>
        /// <returns>Running processes.</returns>
        string[] GetRunningExecutables();

        /// <summary>
        /// Stops the specified executable.
        /// </summary>
        /// <param name="executable">The executable.</param>
        void Stop(string executable);

        /// <summary>
        /// Starts the specified v.
        /// </summary>
        /// <param name="executable">The executable.</param>
        void Start(string executable);
    }
}
