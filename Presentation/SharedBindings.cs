// <copyright file="SharedBindings.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Presentation
{
    using Infrastructure.Common;
    using Infrastructure.Providers;
    using Infrastructure.Serialization;
    using Logic;
    using Logic.Common;
    using Logic.Providers;
    using Logic.Serialization;
    using Ninject.Parameters;

    /// <summary>
    /// Module for dependency injection for the presentation layer.
    /// </summary>
    /// <seealso cref="Ninject.Modules.NinjectModule" />
    public class SharedBindings : Ninject.Modules.NinjectModule
    {
        private readonly string rootPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="SharedBindings" /> class.
        /// </summary>
        /// <param name="rootPath">The root path.</param>
        public SharedBindings(string rootPath)
        {
            this.rootPath = rootPath;
        }

        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            this.Bind<ILogger>()
                .To<ConsoleLogService>()
                .InSingletonScope();

            this.Bind<IPathProvider>()
                .To<PathProvider>()
                .InSingletonScope()
                .WithParameter(new ConstructorArgument("rootPath", this.rootPath));

            this.Bind<IConfigurationService>()
                .To<ConfigurationService>()
                .InSingletonScope();

            this.Bind<IStartupService>()
                .To<BatchFileStartupService>()
                .InSingletonScope();

            this.Bind<IMonitoringService>()
                .To<WindowsMonitoringService>()
                .InSingletonScope();

            this.Bind<ISerializationService>()
                .To<YamlSerializationService>()
                .InSingletonScope();

            this.Bind<LaunchIt>()
                .ToSelf()
                .InSingletonScope();
        }
    }
}
