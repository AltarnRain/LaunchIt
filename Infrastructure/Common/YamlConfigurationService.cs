﻿// <copyright file="YamlConfigurationService.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Common
{
    using Domain.Models.Configuration;
    using Logic.Common;
    using Logic.Extensions;
    using Logic.Providers;
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using YamlDotNet.Serialization;
    using YamlDotNet.Serialization.NamingConventions;

    /// <summary>
    /// Provides a configuration model using a Yaml file.
    /// </summary>
    /// <seealso cref="IConfigurationService" />
    public class YamlConfigurationService : IConfigurationService
    {
        /// <summary>
        /// The path provider.
        /// </summary>
        private readonly IPathProvider pathProvider;

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// The warned about explorer configuration.
        /// </summary>
        private bool warnedAboutExplorerConfiguration;

        /// <summary>
        /// Initializes a new instance of the <see cref="YamlConfigurationService" /> class.
        /// </summary>
        /// <param name="pathProvider">The path provider.</param>
        /// <param name="logger">The logger.</param>
        public YamlConfigurationService(IPathProvider pathProvider, ILogger logger)
        {
            this.pathProvider = pathProvider;
            this.logger = logger;
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

            if (!File.Exists(configurationFile))
            {
                throw new FileNotFoundException(configurationFile);
            }

            var configFileContent = File.ReadAllText(configurationFile);

            var input = new StringReader(configFileContent);

            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(PascalCaseNamingConvention.Instance)
                .Build();

            var model = deserializer.Deserialize<ConfigurationModel>(input);

            if (model.Services is null)
            {
                model.Services = Array.Empty<string>();
            }

            if (model.Executables is null)
            {
                model.Executables = Array.Empty<string>();
            }

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

        /// <summary>
        /// Writes the specified configuration.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public void Write(ConfigurationModel configuration)
        {
            var configurationFile = this.pathProvider.ConfigurationFile();
            var serializer = new SerializerBuilder()
                .WithNamingConvention(PascalCaseNamingConvention.Instance)
                .Build();

            var yamlContent = serializer.Serialize(configuration);

            File.WriteAllText(configurationFile, yamlContent);
        }

        /// <summary>
        /// Edits the in notepad.
        /// </summary>
        public void EditInNotepad()
        {
            var configurationFile = this.pathProvider.ConfigurationFile();

            var processStartInfo = new ProcessStartInfo
            {
                UseShellExecute = true,
                FileName = "notepad.exe",
                Arguments = configurationFile,
            };

            Process.Start(processStartInfo)?.WaitForExit();
        }
    }
}
