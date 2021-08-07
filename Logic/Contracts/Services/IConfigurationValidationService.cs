// <copyright file="IConfigurationValidationService.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Logic.Contracts.Services
{
    using Domain.Models.Configuration;
    using System.Collections.Generic;

    /// <summary>
    /// Contract for a configuration validator.
    /// </summary>
    public interface IConfigurationValidationService
    {
        /// <summary>
        /// Validates the specified configuration model.
        /// </summary>
        /// <param name="configurationModel">The configuration model.</param>
        /// <returns>Warning messages.</returns>
        public IEnumerable<string> Validate(ConfigurationModel configurationModel);
    }
}
