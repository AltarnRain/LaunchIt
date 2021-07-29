// <copyright file="ILaunchModelProvider.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Logic.Contracts.Providers
{
    using Domain.Models.Configuration;

    /// <summary>
    /// Contract for a class that provides a launch model.
    /// </summary>
    public interface ILaunchModelProvider
    {
        /// <summary>
        /// Gets the model.
        /// </summary>
        /// <param name="executableToLaunch">The executable to launch.</param>
        /// <returns>
        /// A LaunchModel.
        /// </returns>
        public LaunchModel GetModel(string executableToLaunch);
    }
}
