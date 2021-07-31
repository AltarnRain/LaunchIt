// <copyright file="HostBuilder.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Presentation
{
    using Domain.Models.Configuration;
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
    public static class HostBuilder
    {
        /// <summary>
        /// Creates the host builder.
        /// </summary>
        /// <param name="rootPath">The root path.</param>
        /// <returns>
        /// An IHostBuilder.
        /// </returns>
        public static IHostBuilder CreateHostBuilder(string rootPath)
        {
            return Host.CreateDefaultBuilder()
                .ConfigureServices((services) =>
                {
                    services
                        .AddSingleton<IPathProvider, PathProvider>(sp => new PathProvider(rootPath))
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
                        .AddSingleton<IStartupService>(serviceProvider =>
                        {
                            var configurationService = serviceProvider.GetRequiredService<IConfigurationService>();
                            var configuration = configurationService.Read();

                            var logger = serviceProvider.GetRequiredService<ILogEventService>();
                            if (configuration.UseBatchFile)
                            {
                                return serviceProvider.GetRequiredService<BatchFileStartupService>();
                            }

                            return serviceProvider.GetRequiredService<LaunchItStartupService>();
                        });
                });
        }
    }
}
