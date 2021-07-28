// <copyright file="ConfigurationDependentBindings.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Presentation
{
    using Domain.Models.Configuration;
    using Infrastructure.Services;
    using Logic.Contracts.Services;

    /// <summary>
    /// Bindings that depend on configuration.
    /// </summary>
    /// <seealso cref="Ninject.Modules.NinjectModule" />
    public class ConfigurationDependentBindings : Ninject.Modules.NinjectModule
    {
        /// <summary>
        /// The configuration.
        /// </summary>
        private readonly ConfigurationModel configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationDependentBindings"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public ConfigurationDependentBindings(ConfigurationModel configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            if (this.configuration.UseBatchFile)
            {
                this.Bind<IStartupService>()
                    .To<BatchFileStartupService>()
                    .InSingletonScope();
            }
            else
            {
                this.Bind<IStartupService>()
                    .To<LaunchItStartupService>()
                    .InSingletonScope();
            }
        }
    }
}
