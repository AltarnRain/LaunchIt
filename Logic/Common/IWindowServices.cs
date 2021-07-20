// <copyright file="IWindowServices.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Logic.Common
{
    using Domain.Models.Services;
    using System.Collections.Generic;

    /// <summary>
    /// Contract for a class that provides windows service information and manipulation.
    /// </summary>
    public interface IWindowServices
    {
        /// <summary>
        /// Gets the services.
        /// </summary>
        /// <returns>ServicesModel's.</returns>
        IEnumerable<ServiceModel> GetServices();

        /// <summary>
        /// Starts the specified service.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <returns>True if succesfull, false otherwise.</returns>
        bool Start(ServiceModel service);

        /// <summary>
        /// Stops the specified service.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <returns>True if succesfull, false otherwise.</returns>
        bool Stop(ServiceModel service);
    }
}
