// <copyright file="TestScope.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Tests.Base
{
    using Domain.Models.Configuration;
    using global::Infrastructure.Factories;
    using global::Infrastructure.Providers;
    using global::Infrastructure.Serialization;
    using global::Infrastructure.Services;
    using Infrastructure.Helpers;
    using Logic.Contracts.Helpers;
    using Logic.Contracts.Providers;
    using Logic.Contracts.Services;
    using Logic.Extensions;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using System;
    using Tests.TestImplementations;

    /// <summary>
    /// Test Scope.
    /// </summary>
    /// <typeparam name="T">Any class.</typeparam>
    /// <seealso cref="IDisposable" />
    public class TestScope : IDisposable
    {
        private readonly IHostBuilder hostBuilder;
        private readonly TestLogger testLogger;
        private readonly IHost host;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestScope" /> class.
        /// </summary>
        /// <param name="rootPath">The root path.</param>
        /// <param name="bindings">The bindings.</param>
        /// <exception cref="Exception">Cannot bind null.</exception>
        public TestScope(string rootPath, BindModel[]? bindings = null)
        {
            this.testLogger = new TestLogger();

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
                        .AddSingleton<IBatchRunnerFactory, TestBatchRunnerFactory>()
                        .AddSingleton<IEditorService, TestEditorService>()
                        .AddSingleton<IProcessWrapper, TestProcessWrapper>()
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

            this.host = this.hostBuilder.Build();

            var loggerService = this.host.Services.GetRequiredService<ILogEventService>();
            loggerService.Subscribe(this.testLogger);
        }

        /// <summary>
        /// Gets the test batch runner factory.
        /// </summary>
        /// <returns>The TestBatchRunnerFactory.</returns>
        public TestBatchRunnerFactory GetTestBatchRunnerFactory()
        {
            return (TestBatchRunnerFactory)this.Get<IBatchRunnerFactory>();
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
            return this.host.Services.GetRequiredService<T>();
        }

        /// <summary>
        /// Gets the logs.
        /// </summary>
        /// <returns>Everything that was logged while a test ran.</returns>
        public string[] GetLogs()
        {
            return this.testLogger.Messages.ToArray();
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
        /// Gets the test process helper.
        /// </summary>
        /// <returns>The test process helper.</returns>
        public TestEditorService GetTestEditorService()
        {
            return (TestEditorService)this.Get<IEditorService>();
        }

        /// <summary>
        /// Gets the test configuration file.
        /// </summary>
        /// <returns>Returns the configuration file name.</returns>
        public string GetTestConfigurationFileName()
        {
            return this.Get<IPathProvider>().ConfigurationFile();
        }

        /// <summary>
        /// Gets the test process wrapper.
        /// </summary>
        /// <returns>The TestProcessWrapper.</returns>
        public TestProcessWrapper GetTestProcessWrapper()
        {
            return (TestProcessWrapper)this.Get<IProcessWrapper>();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.host?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}