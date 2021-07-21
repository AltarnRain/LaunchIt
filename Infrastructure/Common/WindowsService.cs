// <copyright file="WindowsService.cs" company="Antonio Invernizzi V">
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
    /// <seealso cref="Logic.Common.IWindowServices" />
    [SupportedOSPlatform("Windows")]
    public class WindowsService : Logic.Common.IWindowServices
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
        /// <param name="service">The service.</param>
        /// <returns>
        /// True if succesfull, false otherwise.
        /// </returns>
        public bool Start(ServiceModel service)
        {
            return DoActionOnService(service, (actualService) => actualService.Stop());
        }

        /// <summary>
        /// Stops the specified service.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <returns>
        /// True if succesfull, false otherwise.
        /// </returns>
        public bool Stop(ServiceModel service)
        {
            return DoActionOnService(service, (actualService) => actualService.Stop());
        }

        /// <summary>
        /// Does the action.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="action">The action.</param>
        /// <returns>
        /// True if executed, false otherwise.
        /// </returns>
        private static bool DoActionOnService(ServiceModel service, Action<ServiceController> action)
        {
            var actualService = GetActualService(service);
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
        /// <param name="service">The service.</param>
        /// <returns>Returns a ServiceController object.</returns>
        private static ServiceController? GetActualService(ServiceModel service)
        {
            return ServiceController.GetServices().SingleOrDefault(s => s.ServiceName == service.ServiceName);
        }
    }
}
