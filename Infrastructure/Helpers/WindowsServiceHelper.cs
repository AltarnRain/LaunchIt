// <copyright file="WindowsServiceHelper.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Helpers
{
    using Logic.Contracts.Helpers;
    using Logic.Contracts.Services;
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Runtime.Versioning;
    using System.ServiceProcess;

    /// <summary>
    /// Helper class for windows service.
    /// </summary>
    /// <seealso cref="Logic.Contracts.Helpers.IServiceHelper" />
    [SupportedOSPlatform("windows")]
    [ExcludeFromCodeCoverage(Justification = "Wrapper class around ServiceController.")]
    public class WindowsServiceHelper : IServiceHelper
    {
        private readonly ILogEventService logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsServiceHelper" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public WindowsServiceHelper(
            ILogEventService logger)
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
            return GetServices()
                .Where(s => s.Status == ServiceControllerStatus.Running)
                .Select(s => s.DisplayName)
                .ToArray();
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
                this.logger.LogSkipped($"could not find service '{serviceName}'.");
                return;
            }

            if (service.StartType == ServiceStartMode.Disabled)
            {
                this.logger.LogSkipped($"service '{serviceName}' is Disabled. Consider removing it from your configuration file.");
                return;
            }

            if (service.Status == ServiceControllerStatus.Running)
            {
                // Some services throw when attempted to be stopped for various reasons.
                // For example, they might still be starting up.
                // Or, they're being shut down.
                // Either way, this is a fickle but should not throw.
                Retry.Try(
                    () => TryStopService(service),
                    (retryCount) => this.logger.Log($"Failed to stop service '{serviceName}' after {retryCount} attempts."),
                    (retryCount) =>
                    {
                        this.logger.LogStopped($"service '{serviceName}'.");
                    });

                return;
            }

            this.logger.LogSkipped($"service '{serviceName}' is not running.");
        }

        private static bool TryStopService(ServiceController service)
        {
            try
            {
                service.Stop();

                // Give each service a maximum time to stop of 30 seconds.
                // That should be MORE than enough.
                var timeSpan = TimeSpan.FromSeconds(30);
                service.WaitForStatus(ServiceControllerStatus.Stopped, timeSpan);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the service.
        /// </summary>
        /// <returns>Service controllers.</returns>
        private static ServiceController[] GetServices()
        {
            // Abstracted just in case. Originally this method created a cache of service controllers
            // but that broke detection of restarts.
            return ServiceController.GetServices();
        }
    }
}
