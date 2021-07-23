// <copyright file="TestsBindings.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Tests.Base
{
    using global::Infrastructure.Common;
    using global::Infrastructure.Providers;
    using Logic.Common;
    using Logic.Providers;
    using Ninject.Parameters;
    using Tests.TestImplementations;

    /// <summary>
    /// Module for dependency injection for the presentation layer.
    /// </summary>
    /// <seealso cref="Ninject.Modules.NinjectModule" />
    public class TestsBindings : Ninject.Modules.NinjectModule
    {
        private readonly string rootPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestsBindings" /> class.
        /// </summary>
        /// <param name="rootPath">The root path.</param>
        public TestsBindings(string rootPath)
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
                .To<TestRunningProgramsHelper>()
                .InSingletonScope();

            this.Bind<IGameOptimizerActionHandler>()
                .To<GameOptimizerActionHandler>()
                .InSingletonScope();

            this.Bind<IPathProvider>()
                .To<PathProvider>()
                .InSingletonScope()
                .WithParameter(new ConstructorArgument("rootPath", this.rootPath));
        }
    }
}
