// <copyright file="WindowsRunningProgramsHelper.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Common
{
    using Domain.Models.Services;
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
        public IEnumerable<ServiceModel> GetServices()
        {
            var services = ServiceController.GetServices();

            foreach (var service in services)
            {
                yield return new ServiceModel
                {
                    ServiceName = service.ServiceName,
                    DisplayName = service.DisplayName,
                    Running = service.Status == ServiceControllerStatus.Running,
                    Enabled = service.StartType != ServiceStartMode.Disabled,
                    CanStop = service.CanStop,
                    CanPauseAndContinue = service.CanPauseAndContinue,
                    CanShutdown = service.CanShutdown,
                };
            }
        }

        /// <summary>
        /// Starts the specified service.
        /// </summary>
        /// <param name="serviceName">Name of the service.</param>
        /// <returns>
        /// True if succesfull, false otherwise.
        /// </returns>
        public bool StartService(string serviceName)
        {
            return DoActionOnService(serviceName, (actualService) => actualService.Stop());
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
        /// Starts the executable.
        /// </summary>
        /// <param name="executableName">Name of the executable.</param>
        /// <returns>
        /// A boolean.
        /// </returns>
        public bool StartExecutable(string executableName)
        {
            // TODO
            return true;
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
