// <copyright file="ExtensionClass.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Logic.Extensions
{
    using Domain.Models.Configuration;
    using Logic.Providers;
    using System.Diagnostics;

    /// <summary>
    /// Class that provides various extension methods.
    /// </summary>
    public static class ExtensionClass
    {
        /// <summary>
        /// Gets the action file path.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <returns>Location of the actions.txt file.</returns>
        public static string ConfigurationFile(this IPathProvider self)
        {
            return self.MapPath("~/LaunchIt.yml");
        }

        /// <summary>
        /// Starts the monitoring.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <returns>True if the current configuration requires monitoring.</returns>
        public static bool StartMonitoring(this ConfigurationModel self)
        {
            return self.MonitoringConfiguration.MonitorRestarts || self.ServiceShutdownConfiguration.ShutdownRestartedServices;
        }
    }
}
