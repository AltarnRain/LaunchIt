// <copyright file="LaunchModelProvider.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Providers
{
    using Domain.Models.Configuration;
    using Infrastructure.Parsers;
    using Logic.Contracts.Providers;
    using Logic.Contracts.Services;
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Provides a LaunchModel.
    /// </summary>
    /// <seealso cref="Logic.Contracts.Providers.ILaunchModelProvider" />
    public class LaunchModelProvider
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
        /// <param name="args">The arguments.</param>
        /// <returns>
        /// A LaunchModel.
        /// </returns>
        public LaunchModel GetModel(string[] args)
        {
            var configuration = this.configurationService.Read();

            // First, setup a launch model using the configuration file.
            var returnValue = new LaunchModel
            {
                Executables = configuration.Executables,
                ExecutableShutdownConfiguration = configuration.ExecutableShutdownConfiguration,
                Priority = Enum.Parse<ProcessPriorityClass>(configuration.Priority),
                Services = configuration.Services,
                ServiceShutdownConfiguration = configuration.ServiceShutdownConfiguration,
                ShutdownExplorer = configuration.ShutdownExplorer,
                MonitoringConfiguration = configuration.MonitoringConfiguration,
                UseBatchFile = configuration.UseBatchFile,
            };

            // Now, update the configuration based launch model with command line arguments.
            LaunchModelUpdater.UpdateWithCommandLineArguments(args, returnValue);

            return returnValue;
        }
    }
}
