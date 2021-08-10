// <copyright file="ConfigurationValidationService.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Services
{
    using Domain.Models.Configuration;
    using Logic.Contracts.Services;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Validates a configuration model.
    /// </summary>
    /// <seealso cref="Logic.Contracts.Services.IConfigurationValidationService" />
    public class ConfigurationValidationService : IConfigurationValidationService
    {
        private readonly IConfigurationService configurationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationValidationService"/> class.
        /// </summary>
        /// <param name="configurationService">The configuration service.</param>
        public ConfigurationValidationService(IConfigurationService configurationService)
        {
            this.configurationService = configurationService;
        }

        /// <summary>
        /// Validates the specified configuration model.
        /// </summary>
        /// <param name="configurationModel">The configuration model.</param>
        /// <returns>Warning messages.</returns>
        public IEnumerable<string> Validate(ConfigurationModel configurationModel)
        {
            if (configurationModel.Executables.Any(x => x.ToLower().StartsWith("explorer")))
            {
                yield return $"Hey! You've added explorer(.exe) as an executable to shut down. I'll do it but you might want to set to configuration option '{nameof(ConfigurationModel.ShutdownExplorer)}' to true instead.";
                yield return "I've found shutting down explorer as soon as possible works best. Just saying, it's up to you. I'll remind you next time";
            }
        }

        /// <summary>
        /// Validates the specified configuration model.
        /// </summary>
        /// <param name="configurationModel">The configuration model.</param>
        /// <returns>Warning messages.</returns>
        public IEnumerable<string> Validate()
        {
            var configuration = this.configurationService.Read();

            return this.Validate(configuration);
        }
    }
}
