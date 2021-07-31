﻿// <copyright file="LaunchItHostBuilder.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Presentation
{
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

    /// <summary>
    /// Handles dependency injection setup.
    /// </summary>
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
                    services
                        .AddSingleton<IPathProvider, PathProvider>(sp => ActivatorUtilities.CreateInstance<PathProvider>(sp, rootPath))
                        .AddSingleton<ILogEventService, LogEventService>()
                        .AddSingleton<IConfigurationService, ConfigurationService>()
                        .AddSingleton<IMonitoringService, WindowsMonitoringService>()
                        .AddSingleton<ISerializationService, YamlSerializationService>()
                        .AddSingleton<IServiceHelper, WindowsServiceHelper>()
                        .AddSingleton<IProcessHelper, WindowsProcessHelper>()
                        .AddSingleton<LaunchModelProvider>()
                        .AddSingleton<Startup>()
                        .AddSingleton<LaunchIt>()
                        .AddSingleton<LaunchItStartupService>()
                        .AddSingleton<BatchFileStartupService>()
                        .AddSingleton<IStartupService>(sp =>
                        {
                            var configurationService = sp.GetRequiredService<IConfigurationService>();
                            var configuration = configurationService.Read();

                            if (configuration.UseBatchFile)
                            {
                                return sp.GetRequiredService<BatchFileStartupService>();
                            }

                            return sp.GetRequiredService<LaunchItStartupService>();
                        });
                });
        }
    }
}