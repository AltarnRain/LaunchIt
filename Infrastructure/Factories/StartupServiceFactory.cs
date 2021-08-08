// <copyright file="StartupServiceFactory.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Factories
{
    using Domain.Models.Configuration;
    using Infrastructure.Services;
    using Logic.Contracts.Services;
    using Microsoft.Extensions.DependencyInjection;
    using System;

    /// <summary>
    /// Factory for an <see cref="IStartupService"/>.
    /// </summary>
    /// <seealso cref="Logic.Contracts.Services.IStartupServiceFactory" />
    public class StartupServiceFactory : IStartupServiceFactory
    {
        private readonly IServiceProvider serviceProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="StartupServiceFactory"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public StartupServiceFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Creates the specified use batch.
        /// </summary>
        /// <param name="launchModel">The launch model.</param>
        /// <returns>
        /// An IStartupService.
        /// </returns>
        public IStartupService Create(LaunchModel launchModel)
        {
            if (launchModel.UseBatchFile)
            {
                return ActivatorUtilities.CreateInstance<BatchFileStartupService>(this.serviceProvider);
            }

            return ActivatorUtilities.CreateInstance<LaunchItStartupService>(this.serviceProvider);
        }
    }
}
