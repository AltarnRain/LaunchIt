// <copyright file="ProcessWrapper.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Helpers
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Wrapper class for <see cref="Process"/>calls. This is the only place in the program where ProcessStartInfo objects are created.
    /// </summary>
    [ExcludeFromCodeCoverage(Justification = "To close to the Process API to really test.")]
    public static class ProcessWrapper
    {
        /// <summary>
        /// Kills the task.
        /// </summary>
        /// <param name="process">The process.</param>
        /// <returns>Taskkill process.</returns>
        public static IEnumerable<Process?> Kill(string process)
        {
            if (process.Equals(Domain.Constants.KnownProcesses.Explorer, System.StringComparison.OrdinalIgnoreCase) ||
                process.Equals(Domain.Constants.KnownProcesses.ExplorerExe, System.StringComparison.OrdinalIgnoreCase))
            {
                yield return Start("taskkill", $"/f /im {process}");
                yield break;
            }
            else
            {
                foreach (var p in Process.GetProcessesByName(process))
                {
                    p.Kill();
                    yield return p;
                }
            }
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
