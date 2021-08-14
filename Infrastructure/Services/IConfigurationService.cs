// <copyright file="IConfigurationService.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Services
{
    using Domain.Models.Configuration;

    /// <summary>
    /// Contract for a configuration service..
    /// </summary>
    public interface IConfigurationService
    {
        /// <summary>
        /// Reads the default configuration file.
        /// </summary>
        /// <param name="configurationFile">The configuration file.</param>
        /// <returns>A ConfigurationModel.</returns>
        /// <exception cref="FileNotFoundException">Throw if the file is not found.</exception>
        public ConfigurationModel Read();

        /// <summary>
        /// Writes the specified configuration.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public void Write(ConfigurationModel configuration);

        /// <summary>
        /// Writes the example configuration file.
        /// </summary>
        /// <returns>True if an example configuration file was written. False otherwise.</returns>
        bool WriteExampleConfigurationFile();
    }
}
