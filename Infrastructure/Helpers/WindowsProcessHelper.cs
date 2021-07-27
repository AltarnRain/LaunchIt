// <copyright file="WindowsProcessHelper.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Helpers
{
    using Logic.Handlers;
    using Logic.Helpers;
    using Logic.Services;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Process helper for windows.
    /// </summary>
    /// <seealso cref="Logic.Helpers.IProcessHelper" />
    public class WindowsProcessHelper : StopHelperBase, IProcessHelper
    {
        private readonly ILoggerService logger;

        /// <summary>
        /// The ignored processes. These services throw an exception when accessing Process.MainModule.
        /// </summary>
        private readonly List<string> ignoredProcesses = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsProcessHelper" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public WindowsProcessHelper(ILoggerService logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Gets the running processes.
        /// </summary>
        /// <returns>
        /// Running processes.
        /// </returns>
        public string[] GetRunningExecutables()
        {
            var returnValue = new List<string>();

            var processes = Process.GetProcesses()
                .Where(p => !this.ignoredProcesses.Contains(p.ProcessName));

            foreach (var process in processes)
            {
                try
                {
                    var mainModule = process.MainModule;
                    if (mainModule is null)
                    {
                        continue;
                    }

                    var fileName = mainModule.FileName;

                    if (fileName is null)
                    {
                        continue;
                    }

                    var fileNameOnly = Path.GetFileName(fileName);
                    if (fileNameOnly is null)
                    {
                        continue;
                    }

                    returnValue.Add(fileNameOnly);
                }
                catch
                {
                    // Swallow. If the MainModule is not accecible, skip the process. Add it to the ignore list for next time.
                    // Eventually we'll have no exceptions when obtaining running executables.
                    if (!this.ignoredProcesses.Contains(process.ProcessName))
                    {
                        this.logger.Log($"Added process {process.ProcessName} to ignore list. Unable to obtain the name of the executable.");
                        this.ignoredProcesses.Add(process.ProcessName);
                    }
                }
            }

            return returnValue.ToArray();
        }

        /// <summary>
        /// Stops the specified executable.
        /// </summary>
        /// <param name="executable">The executable.</param>
        public void Start(string executable)
        {
            var processStartInfo = new ProcessStartInfo
            {
                UseShellExecute = true,
                FileName = executable,
                CreateNoWindow = true,
            };

            Process.Start(processStartInfo);
            this.logger.Log($"Started '{executable}'.");
        }

        /// <summary>
        /// Stops the specified executable.
        /// </summary>
        /// <param name="executable">The executable.</param>
        public override void Stop(string executable)
        {
            var processStartInfo = new ProcessStartInfo
            {
                UseShellExecute = true,
                FileName = "taskkill",
                Arguments = $"/f /im {executable}",
                CreateNoWindow = true,
            };

            Process.Start(processStartInfo);
            this.AddToStopCount(executable);
            this.logger.Log($"Stopped '{executable}' ({this.GetStopCount(executable)})");
        }
    }
}
