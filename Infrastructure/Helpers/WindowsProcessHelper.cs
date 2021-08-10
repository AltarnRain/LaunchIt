// <copyright file="WindowsProcessHelper.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Helpers
{
    using Logic.Contracts.Helpers;
    using Logic.Contracts.Services;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Process helper for windows.
    /// </summary>
    /// <seealso cref="Logic.Contracts.Helpers.IProcessHelper" />
    [ExcludeFromCodeCoverage(Justification = "To close to the Process API to really test.")]
    public class WindowsProcessHelper : IProcessHelper
    {
        private const int AccessDenied = 5;
        private const int UnableToEnumerateModules = -2147467259;
        private const int ReadOrWriteProcessRequestFail = 299;

        private readonly ILogEventService logger;
        private readonly IProcessWrapper processWrapper;
        private readonly Dictionary<string, string> processFileNameCache = new();

        /// <summary>
        /// The ignored processes. These services throw an exception when accessing Process.MainModule.
        /// </summary>
        private readonly List<string> ignoredProcesses = new()
        {
            Domain.Constants.KnownProcesses.TaskKill, // used to shut down processes.
            Domain.Constants.KnownProcesses.Self, // This is us.
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsProcessHelper" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="processWrapper">The process wrapper.</param>
        public WindowsProcessHelper(ILogEventService logger, IProcessWrapper processWrapper)
        {
            this.logger = logger;
            this.processWrapper = processWrapper;
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

            var processes = Process.GetProcesses().Where(p => !this.ignoredProcesses.Contains(p.ProcessName));

            foreach (var process in processes)
            {
                // Save us some work if we've already looked up the executable.
                if (this.processFileNameCache.ContainsKey(process.ProcessName))
                {
                    returnValue.Add(this.processFileNameCache[process.ProcessName]);
                    continue;
                }

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

                    if (returnValue.Contains(fileNameOnly))
                    {
                        continue;
                    }

                    this.processFileNameCache.Add(process.ProcessName, fileNameOnly);
                    returnValue.Add(fileNameOnly);
                }
                catch (System.ComponentModel.Win32Exception ex)
                {
                    // Win32Exception is pretty generic. Luckly the native code can be used
                    // to determine what went wrong. This way we can decide which processes to ignore.
                    switch (ex.NativeErrorCode)
                    {
                        case AccessDenied:
                        case UnableToEnumerateModules:
                            // Error such as these are thrown when accessing processes like 'System', 'Idle', etc.
                            this.logger.Log($"Process '{process.ProcessName}' will be ignored.");
                            this.ignoredProcesses.Add(process.ProcessName);
                            break;
                        case ReadOrWriteProcessRequestFail:
                            // Ignore this one. Can happen when a process has just been killed.
                            break;
                        default:
                            // Unknown reason the process information could not be determine. Log it and diagnose it.
                            this.logger.Log("Error: " + process.ProcessName);
                            this.logger.Log("   Exception type :" + ex.GetType().FullName);
                            this.logger.Log("   Native code: " + ex.NativeErrorCode.ToString());
                            this.logger.Log("   Message: " + ex.Message);
                            break;
                    }
                }
                catch (System.InvalidOperationException)
                {
                    // Swallow.
                    // Thrown when a process has already exited.
                }
                catch (System.Exception ex)
                {
                    // Exception garbage shute.
                    this.logger.Log("Error: " + process.ProcessName);
                    this.logger.Log("   Exception type :" + ex.GetType().FullName);
                    this.logger.Log("   Message: " + ex.Message);
                }
            }

            return returnValue.ToArray();
        }

        /// <summary>
        /// Stops the specified executable.
        /// </summary>
        /// <param name="executable">The executable.</param>
        /// <returns>A started process.</returns>
        public Process? Start(string executable)
        {
            this.logger.Log($"Started '{executable}'.");
            return this.processWrapper.Start(executable);
        }

        /// <summary>
        /// Stops the specified executable.
        /// </summary>
        /// <param name="executable">The executable.</param>
        public void Stop(string executable)
        {
            this.processWrapper.Kill(executable);
            this.logger.LogStopped($"'{executable}'");
        }
    }
}
