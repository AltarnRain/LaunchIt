// <copyright file="ConfigurationService.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Services
{
    using Domain.Models.Configuration;
    using Logic.Contracts.Providers;
    using Logic.Contracts.Services;
    using Logic.Extensions;
    using System.IO;

    /// <summary>
    /// Provides configuration services.
    /// </summary>
    /// <seealso cref="IConfigurationService" />
    public class ConfigurationService : IConfigurationService
    {
        private readonly IPathProvider pathProvider;
        private readonly ILogEventService logger;
        private readonly ISerializationService serializationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationService" /> class.
        /// </summary>
        /// <param name="pathProvider">The path provider.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="serializationService">The serialization service.</param>
        /// <param name="editorService">The editor service.</param>
        public ConfigurationService(
            IPathProvider pathProvider,
            ILogEventService logger,
            ISerializationService serializationService)
        {
            this.pathProvider = pathProvider;
            this.logger = logger;
            this.serializationService = serializationService;
        }

        /// <summary>
        /// Configurations the file exists.
        /// </summary>
        /// <returns>
        /// True if the configuration file exists.
        /// </returns>
        public bool ConfigurationFileExists()
        {
            var configurationFile = this.pathProvider.ConfigurationFile();
            return File.Exists(configurationFile);
        }

        /// <summary>
        /// Writes the example configuration file.
        /// </summary>
        public void WriteExampleConfigurationFile()
        {
            var configurationModel = new ConfigurationModel
            {
                Executables = new[] { "Example executable 1", "Example executable 2" },
                Services = new[] { "Example Service 1", "Example Service 2" },
            };

            this.Write(configurationModel);
        }

        /// <summary>
        /// Reads the default configuration file and returns a model for it.
        /// </summary>
        /// <returns>A ConfigurationModel.</returns>
        public ConfigurationModel Read()
        {
            var configurationFile = this.pathProvider.ConfigurationFile();

            // No file exists yet. Return the default configuration.
            if (!File.Exists(configurationFile))
            {
                this.logger.Log($"Could not find {configurationFile}. Using default configuration. Run me with the '-reset' argument to reset your settings to default. Use the '-edit' command line argument to open your settings file.");
                return new ConfigurationModel { ShutdownExplorer = false };
            }

            try
            {
                var configFileContent = File.ReadAllText(configurationFile);

                var model = this.serializationService.Deserialize<ConfigurationModel>(configFileContent);

                return model;
            }
            catch (System.Exception ex)
            {
                // Swallow. Always return a configuration model.
                this.logger.Log("Hmmm, looks like there's a problem with your configuration file.");
                this.logger.Log("Here's the exception message, I'll use default settings for now but I won't shut down explorer.");
                this.logger.Log(ex.Message);

                return new ConfigurationModel { ShutdownExplorer = false };
            }
        }

        /// <summary>
        /// Writes the specified configuration.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public void Write(ConfigurationModel configuration)
        {
            var configurationFile = this.pathProvider.ConfigurationFile();
            var content = this.serializationService.Serialize(configuration);

            File.WriteAllText(configurationFile, content);
        }
    }
}
