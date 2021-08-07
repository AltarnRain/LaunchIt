// <copyright file="IProcessHelper.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Logic.Contracts.Helpers
{
    using System.Diagnostics;

    /// <summary>
    /// Contract for a process helper.
    /// </summary>
    public interface IProcessHelper : IStopHelper
    {
        /// <summary>
        /// Gets the running processes.
        /// </summary>
        /// <returns>Running processes.</returns>
        string[] GetRunningExecutables();

        /// <summary>
        /// Starts something.
        /// </summary>
        /// <param name="name">Name of what you want to start.</param>
        /// <returns>A started process.</returns>
        Process? Start(string name);
    }
}
