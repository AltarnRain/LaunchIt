// <copyright file="IConfigFileProvider.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Services
{
    /// <summary>
    /// Provides a full path to a config file.
    /// </summary>
    public interface IConfigFileProvider
    {
        /// <summary>
        /// Gets the configuration file.
        /// </summary>
        /// <returns>The current config file.</returns>
        string GetConfigFile();
    }
}