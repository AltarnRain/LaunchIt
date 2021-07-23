// <copyright file="PresentationBindings.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Presentation
{
    using Infrastructure.Common;
    using Infrastructure.Providers;
    using Logic;
    using Logic.Common;
    using Logic.Providers;
    using Ninject.Parameters;

    /// <summary>
    /// Module for dependency injection for the presentation layer.
    /// </summary>
    /// <seealso cref="Ninject.Modules.NinjectModule" />
    public class PresentationBindings : Ninject.Modules.NinjectModule
    {
        private readonly string rootPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="PresentationBindings" /> class.
        /// </summary>
        /// <param name="rootPath">The root path.</param>
        public PresentationBindings(string rootPath)
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

            this.Bind<IRunningProgramsHelper>()
                .To<WindowsRunningProgramsHelper>()
                .InSingletonScope();

            this.Bind<IGameOptimizerActionHandler>()
                .To<GameOptimizerActionHandler>()
                .InSingletonScope();

            this.Bind<IPathProvider>()
                .To<PathProvider>()
                .InSingletonScope()
                .WithParameter(new ConstructorArgument("rootPath", this.rootPath));

            this.Bind<IActionModelProvider>()
                .To<FileActionModelProvider>()
                .InSingletonScope();

            this.Bind<Startup>()
                .ToSelf()
                .InSingletonScope();
        }
    }
}
