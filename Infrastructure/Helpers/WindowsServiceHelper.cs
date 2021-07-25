// <copyright file="WindowsServiceHelper.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Helpers
{
    using Logic.Helpers;
    using System.Linq;
    using System.Runtime.Versioning;
    using System.ServiceProcess;

    /// <summary>
    /// Helper class for windows service.
    /// </summary>
    /// <seealso cref="Logic.Helpers.IServiceHelper" />
    [SupportedOSPlatform("windows")]
    public class WindowsServiceHelper : IServiceHelper
    {
        /// <summary>
        /// Gets the running services.
        /// </summary>
        /// <returns>
        /// List is service names that have status 'Running'.
        /// </returns>
        public string[] GetRunningServices()
        {
            return ServiceController
                .GetServices()
                .Where(s => s.Status == ServiceControllerStatus.Running)
                .Select(s => s.DisplayName)
                .ToArray();
        }

        /// <summary>
        /// Determines whether the specified service name is running.
        /// </summary>
        /// <param name="serviceName">Name of the service.</param>
        /// <returns>
        /// <c>true</c> if the specified service name is running; otherwise, <c>false</c>.
        /// </returns>
        public bool IsRunning(string serviceName)
        {
            var service = ServiceController.GetServices().SingleOrDefault(s => s.DisplayName == serviceName);

            if (service is null)
            {
                return false;
            }

            return service.Status == ServiceControllerStatus.Running;
        }
    }
}
