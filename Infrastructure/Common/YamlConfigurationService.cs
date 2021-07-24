// <copyright file="YamlConfigurationService.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Common
{
    using Domain.Models.Configuration;
    using Logic.Common;
    using Logic.Extensions;
    using Logic.Providers;
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
        /// Reads the default configuration file and returns a model for it.
        /// </summary>
        /// <returns>A ConfigurationModel.</returns>
        public ConfigurationModel Read()
        {
            var configFile = this.pathProvider.ConfigurationFile();
            return this.Read(configFile);
        }

        /// <summary>
        /// Reads the specified configuration file.
        /// </summary>
        /// <param name="configurationFile">The configuration file.</param>
        /// <returns>A ConfigurationModel.</returns>
        /// <exception cref="FileNotFoundException">Throw if the file is not found.</exception>
        public ConfigurationModel Read(string configurationFile)
        {
            if (File.Exists(configurationFile))
            {
                throw new FileNotFoundException(configurationFile);
            }

            var configFileContent = File.ReadAllText(configurationFile);

            var input = new StringReader(configFileContent);

            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
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
            this.Write(configurationFile, configuration);
        }

        /// <summary>
        /// Writes the specified configuration file.
        /// </summary>
        /// <param name="configurationFile">The configuration file.</param>
        /// <param name="configuration">The configuration.</param>
        public void Write(string configurationFile, ConfigurationModel configuration)
        {
            var serializer = new SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

            var yamlContent = serializer.Serialize(configuration);

            File.WriteAllText(configurationFile, yamlContent);
        }
    }
}
