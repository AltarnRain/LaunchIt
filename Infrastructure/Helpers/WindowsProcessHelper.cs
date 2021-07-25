// <copyright file="WindowsProcessHelper.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Helpers
{
    using Logic.Helpers;
    using System.Diagnostics;
    using System.Linq;

    /// <summary>
    /// Process helper for windows.
    /// </summary>
    /// <seealso cref="Logic.Helpers.IProcessHelper" />
    public class WindowsProcessHelper : IProcessHelper
    {
        /// <summary>
        /// Gets the running processes.
        /// </summary>
        /// <returns>
        /// Running processes.
        /// </returns>
        public string[] GetRunningProcesses()
        {
            return Process
                .GetProcesses()
                .Select(p => p.ProcessName)
                .ToArray();
        }

        /// <summary>
        /// Determines whether the specified process name is running.
        /// </summary>
        /// <param name="processName">Name of the process.</param>
        /// <returns>
        /// <c>true</c> if the specified process name is running; otherwise, <c>false</c>.
        /// </returns>
        public bool IsRunning(string processName)
        {
            var process = Process.GetProcessesByName(processName);

            if (process is null || process.Length == 0)
            {
                return false;
            }

            return true;
        }
    }
}
