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
    public class WindowsServiceHelper : StopHelperBase, IServiceHelper
    {
        private readonly ILoggerService logger;

        private ServiceController[]? serviceControllers;

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
            return this.GetServices()
                .Where(s => s.Status == ServiceControllerStatus.Running)
                .Select(s => s.DisplayName)
                .ToArray();
        }

        /// <summary>
        /// Gets the service.
        /// </summary>
        /// <param name="serviceName">Name of the service.</param>
        /// <param name="trackCount">if set to <c>true</c> [track count].</param>
        public override void Stop(string serviceName, bool trackCount)
        {
            var service = ServiceController.GetServices().SingleOrDefault(s => s.DisplayName == serviceName);

            if (service is null)
            {
                this.logger.LogSkipped($"could not find service '{serviceName}'.");
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
                        if (trackCount)
                        {
                            this.AddToStopCount(serviceName);
                            this.logger.LogStopped($"service '{serviceName}' ({this.GetStopCount(serviceName)}).");
                            return;
                        }

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
        private ServiceController[] GetServices()
        {
            if (this.serviceControllers is null)
            {
                this.serviceControllers = ServiceController.GetServices();
            }

            return this.serviceControllers;
        }
    }
}
