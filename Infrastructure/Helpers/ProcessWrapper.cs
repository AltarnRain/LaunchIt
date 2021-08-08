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
    public class ProcessWrapper : IProcessWrapper
    {
        /// <summary>
        /// Kills the task.
        /// </summary>
        /// <param name="process">The process.</param>
        public void Kill(string process)
        {
            if (process.Equals(Domain.Constants.KnownProcesses.Explorer, System.StringComparison.OrdinalIgnoreCase) ||
                process.Equals(Domain.Constants.KnownProcesses.ExplorerExe, System.StringComparison.OrdinalIgnoreCase))
            {
                var p = this.Start("taskkill", $"/f /im {process}");
                p?.WaitForExit();
            }
            else
            {
                foreach (var p in Process.GetProcessesByName(GetProcessName(process)))
                {
                    p.Kill();

                    // Make it sync. https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.process.kill?view=net-5.0
                    p.WaitForExit(30000);
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
        public Process? Start(string executable, string? arguments = null)
        {
            var processStartInfo = GetBaseProcessInfo(executable, arguments);

            try
            {
                // Catch exceptions. If the process cannot be started, e.g., the executable cannot be found we do not want
                // the program to crash, we want to report the problem to the user.
                return Process.Start(processStartInfo);
            }
            catch (System.Exception)
            {
                // Return null to signal the outside works this process did not start. The return type is nullable by
                // contract so null checks are required. I. Love. Nullable.
                return null;
            }
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

        private static string GetProcessName(string process)
        {
            // If the 'process' ends wit .exe, strip .exe off. Process names do not end with .exe
            if (process.EndsWith(".exe", System.StringComparison.OrdinalIgnoreCase))
            {
                return process[0..^4];
            }

            return process;
        }
    }
}
