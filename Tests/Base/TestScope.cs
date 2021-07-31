// <copyright file="TestScope.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Tests.Base
{
    using Infrastructure.Providers;
    using Infrastructure.Serialization;
    using Infrastructure.Services;
    using Logic.Contracts.Providers;
    using Logic.Contracts.Services;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Test Scope.
    /// </summary>
    /// <typeparam name="T">Any class.</typeparam>
    /// <seealso cref="System.IDisposable" />
    public class TestScope : IDisposable
    {
        private readonly IHostBuilder hostBuilder;
        private IHost? host;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestScope"/> class.
        /// </summary>
        /// <param name="rootPath">The root path.</param>
        public TestScope(string rootPath)
        {
            // Setup the most basic services.
            this.hostBuilder = Host.CreateDefaultBuilder()
                .ConfigureServices((services) =>
                {
                    services
                        .AddSingleton<IPathProvider, PathProvider>(sp => ActivatorUtilities.CreateInstance<PathProvider>(sp, rootPath))
                        .AddSingleton<ILogEventService, LogEventService>()
                        .AddSingleton<IConfigurationService, ConfigurationService>()
                        .AddSingleton<ISerializationService, YamlSerializationService>();
                });
        }

        /// <summary>
        /// Starts the specified bindings.
        /// </summary>
        /// <typeparam name="T">Any class.</typeparam>
        /// <param name="bindings">The bindings.</param>
        /// <returns>Instance of T.</returns>
        public T Get<T>(List<BindModel>? bindings = null)
            where T : class
        {
            if (bindings is not null)
            {
                this.hostBuilder.ConfigureServices(services =>
                {
                    foreach (var binding in bindings)
                    {
                        if (binding.ServiceType is null)
                        {
                            throw new Exception("Cannot bind null");
                        }

                        if (binding.ImplementationType is null)
                        {
                            services.AddSingleton(binding.ServiceType);
                            return;
                        }

                        services.AddSingleton(binding.ServiceType, binding.ImplementationType);
                    }
                });
            }

            if (this.host is null)
            {
                this.host = this.hostBuilder.Build();
            }

            return this.host.Services.GetRequiredService<T>();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}