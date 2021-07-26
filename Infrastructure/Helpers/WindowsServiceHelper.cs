// <copyright file="WindowsServiceHelper.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Helpers
{
    using Logic.Helpers;
    using Logic.Services;
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
        private readonly ILoggerService logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsServiceHelper" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public WindowsServiceHelper(
            ILoggerService logger)
        {
            this.logger = logger;
        }

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

        /// <summary>
        /// Gets the service.
        /// </summary>
        /// <param name="serviceName">Name of the service.</param>
        public void Stop(string serviceName)
        {
            var service = ServiceController.GetServices().SingleOrDefault(s => s.DisplayName == serviceName);

            if (service is null)
            {
                this.logger.Log($"Could not find service {serviceName}. Skipping.");
                return;
            }

            if (service.Status == ServiceControllerStatus.Running)
            {
                this.logger.Log($"Stopping service {serviceName}");
                service.Stop();
                return;
            }

            this.logger.Log($"Service {serviceName} is not running. Skipping.");
        }
    }
}
