// <copyright file="ConfigFileProvider.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Services
{
    using Logic.Contracts.Providers;

    /// <summary>
    /// Provides the current config file.
    /// </summary>
    /// <seealso cref="Infrastructure.Services.IConfigFileProvider" />
    public class ConfigFileProvider : IConfigFileProvider
    {
        private readonly string configFile;
        private readonly IPathProvider pathProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigFileProvider"/> class.
        /// </summary>
        /// <param name="configFile">The configuration file.</param>
        /// <param name="pathProvider">The path provider.</param>
        public ConfigFileProvider(
            string configFile,
            IPathProvider pathProvider)
        {
            this.configFile = configFile;
            this.pathProvider = pathProvider;
        }

        /// <summary>
        /// Gets the configuration file.
        /// </summary>
        /// <returns>
        /// The current config file.
        /// </returns>
        public string GetConfigFile()
        {
            return this.pathProvider.MapPath($"~/{this.configFile}");
        }
    }
}
