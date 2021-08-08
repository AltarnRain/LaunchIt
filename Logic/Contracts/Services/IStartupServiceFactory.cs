// <copyright file="IStartupServiceFactory.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Logic.Contracts.Services
{
    using Domain.Models.Configuration;

    /// <summary>
    /// Contract for a factory class that creates an IStartupService.
    /// </summary>
    public interface IStartupServiceFactory
    {
        /// <summary>
        /// Creates the specified use batch.
        /// </summary>
        /// <param name="launchModel">The launch model.</param>
        /// <returns>
        /// An IStartupService.
        /// </returns>
        public IStartupService Create(LaunchModel launchModel);
    }
}
