// <copyright file="TestConfigFileProvider.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Tests.TestImplementations
{
    using Infrastructure.Services;
    using Logic.Contracts.Providers;

    /// <summary>
    /// Test implementation for <see cref="IConfigFileProvider"/>.
    /// </summary>
    /// <seealso cref="Infrastructure.Services.IConfigFileProvider" />
    public class TestConfigFileProvider : IConfigFileProvider
    {
        /// <summary>
        /// The path provider.
        /// </summary>
        private readonly IPathProvider pathProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestConfigFileProvider"/> class.
        /// </summary>
        /// <param name="pathProvider">The path provider.</param>
        public TestConfigFileProvider(
            IPathProvider pathProvider)
        {
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
            return this.pathProvider.MapPath($"~/{Domain.Constants.ConfigurationConstants.DefaultConfigurationFileName}");
        }
    }
}
