// <copyright file="BatchRunnerFactory.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Factories
{
    using Infrastructure.Helpers;
    using Microsoft.Extensions.DependencyInjection;
    using System;

    /// <summary>
    /// Factory class for an <see cref="IBatchRunner"/>.
    /// </summary>
    public class BatchRunnerFactory : IBatchRunnerFactory
    {
        private readonly IServiceProvider serviceProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="BatchRunnerFactory" /> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public BatchRunnerFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Creates the specified batch content.
        /// </summary>
        /// <param name="batchContent">Content of the batch.</param>
        /// <returns>An IBatchRunner.</returns>
        public IBatchRunner Create(string batchContent)
        {
            return ActivatorUtilities.CreateInstance<BatchRunner>(this.serviceProvider, batchContent);
        }
    }
}
