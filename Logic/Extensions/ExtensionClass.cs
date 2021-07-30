﻿// <copyright file="ExtensionClass.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Logic.Extensions
{
    using Domain.Models.Configuration;
    using Logic.Contracts.Providers;

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
        /// Returns true of the LaunchIt's configuration requires the monitor service to run.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <returns>True if the current configuration requires monitoring.</returns>
        public static bool StartMonitoring(this LaunchModel self)
        {
            return self.MonitoringConfiguration.MonitorRestarts ||
                self.ServiceShutdownConfiguration.ShutdownAfterRestart ||
                self.ExecutableShutdownConfiguration.ShutdownAfterRestart;
        }

        /// <summary>
        /// Determines whether this instance is switch.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <returns>
        ///   <c>true</c> if the specified self is switch; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsSwitch(this string self)
        {
            return self.StartsWith("/") || self.StartsWith("-");
        }
    }
}
