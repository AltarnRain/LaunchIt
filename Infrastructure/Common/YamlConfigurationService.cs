// <copyright file="YamlConfigurationService.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Common
{
    using Domain.Models.Configuration;
    using Logic.Common;
    using Logic.Extensions;
    using Logic.Providers;
    using System.Diagnostics;
    using System.IO;
    using YamlDotNet.Serialization;
    using YamlDotNet.Serialization.NamingConventions;

    /// <summary>
    /// Provides a configuration model using a Yaml file.
    /// </summary>
    /// <seealso cref="IConfigurationService" />
    public class YamlConfigurationService : IConfigurationService
    {
        private readonly IPathProvider pathProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="YamlConfigurationService"/> class.
        /// </summary>
        /// <param name="pathProvider">The path provider.</param>
        public YamlConfigurationService(IPathProvider pathProvider)
        {
            this.pathProvider = pathProvider;
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

            return deserializer.Deserialize<ConfigurationModel>(input);
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
