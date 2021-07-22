// <copyright file="TestWindowsService.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Tests.TestImplementations
{
    using Domain.Models.Services;
    using Logic.Common;
    using System.Collections.Generic;

    /// <summary>
    /// Test implementation for IWindowsServices.
    /// </summary>
    /// <seealso cref="Logic.Common.IRunningProgramsHelper" />
    public class TestWindowsService : IRunningProgramsHelper
    {
        /// <summary>
        /// Gets or sets the serviemodels to return.
        /// </summary>
        public List<ServiceModel> ServiemodelsToReturn { get; set; } = new();

        /// <summary>
        /// Gets a value indicating whether [start called].
        /// </summary>
        public bool ServiceStartCalled { get; private set; }

        /// <summary>
        /// Gets a value indicating whether [stop called].
        /// </summary>
        public bool ServiceStopCalled { get; private set; }

        /// <summary>
        /// Gets a value indicating whether [start called].
        /// </summary>
        public bool StartExecutableCalled { get; private set; }

        /// <summary>
        /// Gets a value indicating whether [stop called].
        /// </summary>
        public bool StopExecutableCalled { get; private set; }

        /// <summary>
        /// Gets the services.
        /// </summary>
        /// <returns>Service Model's.</returns>
        public IEnumerable<ServiceModel> GetServices()
        {
            return this.ServiemodelsToReturn;
        }

        /// <summary>
        /// Starts the executable.
        /// </summary>
        /// <param name="executableName">Name of the executable.</param>
        /// <returns>A boolean.</returns>
        public bool StartExecutable(string executableName)
        {
            this.StartExecutableCalled = true;
            return true;
        }

        /// <summary>
        /// Starts the specified service.
        /// </summary>
        /// <param name="serviceName">Name of the service.</param>
        /// <returns>
        /// A boolean.
        /// </returns>
        public bool StartService(string serviceName)
        {
            this.ServiceStartCalled = true;
            return true;
        }

        /// <summary>
        /// Stops the executable.
        /// </summary>
        /// <param name="executableName">Name of the executable.</param>
        /// <returns>A boolean.</returns>
        public bool StopExecutable(string executableName)
        {
            this.StopExecutableCalled = true;
            return true;
        }

        /// <summary>
        /// Stops the specified service.
        /// </summary>
        /// <param name="serviceName">Name of the service.</param>
        /// <returns>
        /// A Boolean.
        /// </returns>
        public bool StopService(string serviceName)
        {
            this.ServiceStopCalled = true;
            return true;
        }
    }
}
