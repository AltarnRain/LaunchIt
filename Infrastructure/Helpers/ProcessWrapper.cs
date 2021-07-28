// <copyright file="ProcessWrapper.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Helpers
{
    using System.Diagnostics;

    /// <summary>
    /// Wrapper class for <see cref="Process"/>calls. This is the only place in the program where ProcessStartInfo objects are created.
    /// </summary>
    public static class ProcessWrapper
    {
        /// <summary>
        /// Kills the task.
        /// </summary>
        /// <param name="process">The process.</param>
        /// <returns>Taskkill process.</returns>
        public static Process? Kill(string process)
        {
            return Start("taskkill", $"/f /im {process}");
        }

        /// <summary>
        /// Starts the specified executable.
        /// </summary>
        /// <param name="executable">The executable.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>
        /// Started process.
        /// </returns>
        public static Process? Start(string executable, string? arguments = null)
        {
            var processStartInfo = GetBaseProcessInfo(executable, arguments);

            return Process.Start(processStartInfo);
        }

        private static ProcessStartInfo GetBaseProcessInfo(string executable, string? arguments)
        {
            return new ProcessStartInfo
            {
                UseShellExecute = true,
                CreateNoWindow = true,
                FileName = executable,
                Arguments = arguments ?? string.Empty,
            };
        }
    }
}
