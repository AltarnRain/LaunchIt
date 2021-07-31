// <copyright file="TestScope.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Tests.Base
{
    using Domain.Models.Configuration;
    using Infrastructure.Providers;
    using Infrastructure.Serialization;
    using Infrastructure.Services;
    using Logic.Contracts.Helpers;
    using Logic.Contracts.Providers;
    using Logic.Contracts.Services;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using System;
    using System.Collections.Generic;
    using Tests.TestImplementations;

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
        /// Initializes a new instance of the <see cref="TestScope" /> class.
        /// </summary>
        /// <param name="rootPath">The root path.</param>
        /// <param name="bindings">The bindings.</param>
        /// <exception cref="Exception">Cannot bind null</exception>
        public TestScope(string rootPath, BindModel[]? bindings = null)
        {
            // Setup the most basic services.
            this.hostBuilder = Host.CreateDefaultBuilder()
                .ConfigureServices((services) =>
                {
                    services
                        .AddSingleton<IPathProvider, PathProvider>(sp => ActivatorUtilities.CreateInstance<PathProvider>(sp, rootPath))
                        .AddSingleton<ILogEventService, LogEventService>()
                        .AddSingleton<IConfigurationService, TestConfigurationService>()
                        .AddSingleton<ISerializationService, YamlSerializationService>()
                        .AddSingleton<IServiceHelper, TestServiceHelper>()
                        .AddSingleton<IProcessHelper, TestProcessHelper>()
                        ;
               });

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
                            continue;
                        }

                        services.AddSingleton(binding.ServiceType, binding.ImplementationType);
                    }
                });
            }
        }

        /// <summary>
        /// Starts the specified bindings.
        /// </summary>
        /// <typeparam name="T">Any class.</typeparam>
        /// <param name="bindings">The bindings.</param>
        /// <returns>Instance of T.</returns>
        public T Get<T>()
            where T : class
        {
            if (this.host is null)
            {
                this.host = this.hostBuilder.Build();
            }

            return this.host.Services.GetRequiredService<T>();
        }

        /// <summary>
        /// Sets the configuration.
        /// </summary>
        /// <param name="configurationModel">The configuration model.</param>
        public void SetConfiguration(ConfigurationModel configurationModel)
        {
            var configurationService = (TestConfigurationService)this.Get<IConfigurationService>();
            configurationService.ConfigurationModel = configurationModel;
        }

        /// <summary>
        /// Gets the test service helper.
        /// </summary>
        /// <returns>The test service helper.</returns>
        public TestServiceHelper GetTestServiceHelper()
        {
            return (TestServiceHelper)this.Get<IServiceHelper>();
        }

        /// <summary>
        /// Gets the test process helper.
        /// </summary>
        /// <returns>The test process helper.</returns>
        public TestProcessHelper GetTestProcessHelper()
        {
            return (TestProcessHelper)this.Get<IProcessHelper>();
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