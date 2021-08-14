// <copyright file="LaunchItHostBuilder.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Presentation
{
    using Infrastructure.Factories;
    using Infrastructure.Helpers;
    using Infrastructure.Providers;
    using Infrastructure.Serialization;
    using Infrastructure.Services;
    using Logic;
    using Logic.Contracts.Helpers;
    using Logic.Contracts.Providers;
    using Logic.Contracts.Services;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Handles dependency injection setup.
    /// </summary>
    [ExcludeFromCodeCoverage(Justification = "DIP Container construction. Used EVERYWHERE, no need to cover seperately.")]
    public static class LaunchItHostBuilder
    {
        /// <summary>
        /// Creates the host builder.
        /// </summary>
        /// <param name="rootPath">The root path.</param>
        /// <returns>
        /// An IHostBuilder.
        /// </returns>
        public static IHostBuilder Create(string rootPath)
        {
            return Host.CreateDefaultBuilder()
                .ConfigureServices((services) =>
                {
                    // Bind abstracted classes.
                    services
                        .AddSingleton<IPathProvider, PathProvider>(sp => ActivatorUtilities.CreateInstance<PathProvider>(sp, rootPath))
                        .AddSingleton<ILogEventService, LogEventService>()
                        .AddSingleton<IConfigurationService, ConfigurationService>()
                        .AddSingleton<IMonitoringService, WindowsMonitoringService>()
                        .AddSingleton<ISerializationService, YamlSerializationService>()
                        .AddSingleton<IServiceHelper, WindowsServiceHelper>()
                        .AddSingleton<IProcessHelper, WindowsProcessHelper>()
                        .AddSingleton<IBatchRunnerFactory, BatchRunnerFactory>()
                        .AddSingleton<IEditorService, EditorService>()
                        .AddSingleton<IConfigurationValidationService, ConfigurationValidationService>()
                        .AddSingleton<IProcessWrapper, ProcessWrapper>()
                        .AddSingleton<IStartupServiceFactory, StartupServiceFactory>()
                        .AddSingleton<IKeypressProvider, ConsoleKeypressProvider>()
                        ;

                    // Bind classes.
                    services
                        .AddSingleton<LaunchModelProvider>()
                        .AddSingleton<Startup>()
                        .AddSingleton<LaunchIt>()
                        .AddSingleton<LaunchItStartupService>()
                        .AddSingleton<BatchFileStartupService>()
                        ;
                });
        }
    }
}
