// <copyright file="TestConfigurationService.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Tests.TestImplementations
{
    using Domain.Models.Configuration;
    using Logic.Contracts.Services;
    using System;

    /// <summary>
    /// Test implementation of <see cref="IConfigurationService"/>.
    /// </summary>
    /// <seealso cref="Logic.Contracts.Services.IConfigurationService" />
    public class TestConfigurationService : IConfigurationService
    {
        /// <summary>
        /// Gets or sets the configuration model.
        /// </summary>
        public ConfigurationModel ConfigurationModel { get; set; } = new ConfigurationModel();

        /// <summary>
        /// Configurations the file exists.
        /// </summary>
        /// <returns>
        /// True if the configuration file exists.
        /// </returns>
        public bool ConfigurationFileExists()
        {
            return true;
        }

        /// <summary>
        /// Edits the in notepad.
        /// </summary>
        public void EditInNotepad()
        {
            // Does nothing.
        }

        /// <summary>
        /// Reads the default configuration file.
        /// </summary>
        /// <returns>
        /// A ConfigurationModel.
        /// </returns>
        public ConfigurationModel Read()
        {
            return this.ConfigurationModel;
        }

        /// <summary>
        /// Writes the specified configuration.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public void Write(ConfigurationModel configuration)
        {
            this.ConfigurationModel = configuration;
        }

        /// <summary>
        /// Writes the example configuration file.
        /// </summary>
        public void WriteExampleConfigurationFile()
        {
            // Does nothing.
        }
    }
}
