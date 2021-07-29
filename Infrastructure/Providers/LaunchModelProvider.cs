// <copyright file="LaunchModelProvider.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Providers
{
    using Domain.Models.Configuration;
    using Logic.Contracts.Providers;
    using Logic.Contracts.Services;
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Provides a LaunchModel.
    /// </summary>
    /// <seealso cref="Logic.Contracts.Providers.ILaunchModelProvider" />
    public class LaunchModelProvider : ILaunchModelProvider
    {
        private readonly IConfigurationService configurationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchModelProvider"/> class.
        /// </summary>
        /// <param name="configurationService">The configuration service.</param>
        public LaunchModelProvider(IConfigurationService configurationService)
        {
            this.configurationService = configurationService;
        }

        /// <summary>
        /// Gets the model.
        /// </summary>
        /// <param name="executableToLaunch">The executable to launch.</param>
        /// <returns>
        /// A LaunchModel.
        /// </returns>
        public LaunchModel GetModel(string executableToLaunch)
        {
            var configuration = this.configurationService.Read();

            return new LaunchModel
            {
                ExecutableToLaunch = executableToLaunch,
                Executables = configuration.Executables,
                ExecutableShutdownConfiguration = configuration.ExecutableShutdownConfiguration,
                Priority = Enum.Parse<ProcessPriorityClass>(configuration.Priority),
                Services = configuration.Services,
                ServiceShutdownConfiguration = configuration.ServiceShutdownConfiguration,
                ShutdownExplorer = configuration.ShutdownExplorer,
                MonitoringConfiguration = configuration.MonitoringConfiguration,
                UseBatchFile = configuration.UseBatchFile,
            };
        }
    }
}
