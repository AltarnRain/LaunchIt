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
        /// Determines whether the specified process name is running.
        /// </summary>
        /// <param name="processName">Name of the process.</param>
        /// <returns>
        ///   <c>true</c> if the specified process name is running; otherwise, <c>false</c>.
        /// </returns>
        bool IsRunning(string processName);
    }
}
