// <copyright file="WindowsRunningProgramsHelper.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Common
{
    using Domain.Models.Programs;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Versioning;
    using System.ServiceProcess;

    /// <summary>
    /// Wrapper class for dealing with window services.
    /// </summary>
    /// <seealso cref="Logic.Common.IRunningProgramsHelper" />
    [SupportedOSPlatform("Windows")]
    public class WindowsRunningProgramsHelper : Logic.Common.IRunningProgramsHelper
    {
        /// <summary>
        /// Gets the services.
        /// </summary>
        /// <returns>
        /// ServicesModel's.
        /// </returns>
        public IEnumerable<ProgramModel> GetRunningPrograms()
        {
            foreach (var x in YieldService())
            {
                yield return x;
            }

            foreach (var x in YieldProcess())
            {
                yield return x;
            }
        }

        /// <summary>
        /// Stops the specified service.
        /// </summary>
        /// <param name="serviceName">Name of the service.</param>
        /// <returns>
        /// True if succesfull, false otherwise.
        /// </returns>
        public bool StopService(string serviceName)
        {
            return DoActionOnService(serviceName, (actualService) => actualService.Stop());
        }

        /// <summary>
        /// Stops the executable.
        /// </summary>
        /// <param name="executableName">Name of the executable.</param>
        /// <returns>
        /// A boolean.
        /// </returns>
        public bool StopExecutable(string executableName)
        {
            // TODO
            return true;
        }

        /// <summary>
        /// Yields the service.
        /// </summary>
        /// <returns>Yield a service in a running program model.</returns>
        private static IEnumerable<ProgramModel> YieldService()
        {
            var services = ServiceController.GetServices();

            foreach (var service in services)
            {
                yield return new ProgramModel
                {
                    Name = service.ServiceName,
                    ProgramType = ProgramType.Service,
                    ServiceInfo = new ServiceInfo
                    {
                        Running = service.Status == ServiceControllerStatus.Running,
                        DisplayName = service.DisplayName,
                        Enabled = service.StartType != ServiceStartMode.Disabled,
                        CanStop = service.CanStop,
                        CanPauseAndContinue = service.CanPauseAndContinue,
                        CanShutdown = service.CanShutdown,
                    },
                };
            }
        }

        private static IEnumerable<ProgramModel> YieldProcess()
        {
            var processes = System.Diagnostics.Process.GetProcesses();

            foreach (var process in processes)
            {
                yield return new ProgramModel
                {
                    Name = process.ProcessName,
                    ProgramType = ProgramType.Executable,
                    Running = true,
                };
            }
        }

        /// <summary>
        /// Does the action.
        /// </summary>
        /// <param name="serviceName">Name of the service.</param>
        /// <param name="action">The action.</param>
        /// <returns>
        /// True if executed, false otherwise.
        /// </returns>
        private static bool DoActionOnService(string serviceName, Action<ServiceController> action)
        {
            var actualService = GetActualService(serviceName);
            if (actualService is null)
            {
                return false;
            }

            action(actualService);
            return true;
        }

        /// <summary>
        /// Gets the actual service.
        /// </summary>
        /// <param name="serviceName">Name of the service.</param>
        /// <returns>
        /// Returns a ServiceController object.
        /// </returns>
        private static ServiceController? GetActualService(string serviceName)
        {
            return ServiceController.GetServices().SingleOrDefault(s => s.ServiceName == serviceName);
        }
    }
}
