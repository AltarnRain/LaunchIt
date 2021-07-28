// <copyright file="ConfigurationService.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Services
{
    using Domain.Models.Configuration;
    using Infrastructure.Helpers;
    using Logic.Extensions;
    using Logic.Providers;
    using Logic.Serialization;
    using Logic.Services;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Provides configuration services.
    /// </summary>
    /// <seealso cref="IConfigurationService" />
    public class ConfigurationService : IConfigurationService
    {
        /// <summary>
        /// The path provider.
        /// </summary>
        private readonly IPathProvider pathProvider;

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILoggerService logger;

        /// <summary>
        /// The serialization service.
        /// </summary>
        private readonly ISerializationService serializationService;

        /// <summary>
        /// The warned about explorer configuration.
        /// </summary>
        private bool warnedAboutExplorerConfiguration;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationService" /> class.
        /// </summary>
        /// <param name="pathProvider">The path provider.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="serializationService">The serialization service.</param>
        public ConfigurationService(IPathProvider pathProvider, ILoggerService logger, ISerializationService serializationService)
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
                this.logger.Log($"Could not find {configurationFile}. Using default configuration. Run LaunchIt with the 'reset' argument to reset your settings to default. Use the 'edit' command line argument to open your settings file.");
                return new ConfigurationModel();
            }

            try
            {
                var configFileContent = File.ReadAllText(configurationFile);

                var model = this.serializationService.Deserialize<ConfigurationModel>(configFileContent);

                // check if the user added explorer.exe to executables to shutdown. I've added an option to the configuration
                // model to specificall do this. Reason being that explorer can reboot servives when it thinks it needs them
                // so its best to shut it down right away. Also, the program can restart explorer.exe that way.
                if (!this.warnedAboutExplorerConfiguration && model.Executables.Any(x => x.ToLower().StartsWith("explorer")))
                {
                    this.logger.Log("Hey! You've added explorer(.exe) as an executable to shut down. I'll do it but you might want to set to configuration option 'ShutdownExplorer' to true instead.");
                    this.logger.Log("I've found shutting down explorer as soon as possible works best. Just saying, it's up to you. I'll remind you next time");

                    this.warnedAboutExplorerConfiguration = true;
                }

                return model;
            }
            catch (System.Exception ex)
            {
                // Swallow. Always return a configuration model.
                this.logger.Log("Hmmm, looks like there's a problem with your configuration file.");
                this.logger.Log("Here's the exception message, we'll use default settings for now.");
                this.logger.Log(ex.Message);

                return new ConfigurationModel();
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

        /// <summary>
        /// Edits the in notepad.
        /// </summary>
        public void EditInNotepad()
        {
            var configurationFile = this.pathProvider.ConfigurationFile();
            ProcessWrapper.Start(this.GetEditor(), configurationFile)?.WaitForExit();
        }

        private string GetEditor()
        {
            var returnValue = "notepad.exe";

            try
            {
                var configuration = this.Read();
                return configuration.PreferredEditor;
            }
            catch
            {
                // Swallow.
            }

            return returnValue;
        }
    }
}
