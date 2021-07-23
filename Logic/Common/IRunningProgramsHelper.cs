// <copyright file="IRunningProgramsHelper.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Logic.Common
{
    using Domain.Models.Programs;
    using System.Collections.Generic;

    /// <summary>
    /// Contract for a class that provides windows service information and manipulation.
    /// </summary>
    public interface IRunningProgramsHelper
    {
        /// <summary>
        /// Gets the services.
        /// </summary>
        /// <returns>
        /// ServicesModel's.
        /// </returns>
        IEnumerable<ProgramModel> GetRunningPrograms();

        /// <summary>
        /// Stops the specified service.
        /// </summary>
        /// <param name="serviceName">Name of the service.</param>
        /// <returns>
        /// True if succesfull, false otherwise.
        /// </returns>
        bool StopService(string serviceName);

        /// <summary>
        /// Stops the executable.
        /// </summary>
        /// <param name="executableName">Name of the executable.</param>
        /// <returns>A boolean.</returns>
        bool StopExecutable(string executableName);
    }
}
