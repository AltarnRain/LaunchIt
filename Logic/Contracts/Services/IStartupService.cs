// <copyright file="IStartupService.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Logic.Contracts.Services
{
    using Domain.Models.Configuration;
    using System.Diagnostics;

    /// <summary>
    /// Contract for a Startup Service.
    /// </summary>
    public interface IStartupService
    {
        /// <summary>
        /// Starts the specified executable.
        /// </summary>
        /// <param name="launchModel">The launch model.</param>
        /// <returns>
        /// A Process.
        /// </returns>
        public Process Start(LaunchModel launchModel);
    }
}
