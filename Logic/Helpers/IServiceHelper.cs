// <copyright file="IServiceHelper.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Logic.Helpers
{
    /// <summary>
    /// Contract for a service helper.
    /// </summary>
    public interface IServiceHelper
    {
        /// <summary>
        /// Determines whether the specified service name is running.
        /// </summary>
        /// <param name="serviceName">Name of the service.</param>
        /// <returns>
        ///   <c>true</c> if the specified service name is running; otherwise, <c>false</c>.
        /// </returns>
        bool IsRunning(string serviceName);

        /// <summary>
        /// Gets the running services.
        /// </summary>
        /// <returns>List is service names that have status 'Running'.</returns>
        string[] GetRunningServices();

        /// <summary>
        /// Gets the service.
        /// </summary>
        /// <param name="serviceName">Name of the service.</param>
        void Stop(string serviceName);
    }
}
