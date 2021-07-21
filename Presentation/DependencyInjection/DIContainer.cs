// <copyright file="DIContainer.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace GameLauncher.DependencyInjection
{
    using Infrastructure.Common;
    using Infrastructure.Providers;
    using Logic.Common;
    using Logic.Providers;
    using StrongInject;

    /// <summary>
    /// Dependency injection container.
    /// </summary>
    /// <seealso cref="IContainer{Logic.Startup}" />
    [Register(typeof(Logic.Startup), Scope.SingleInstance)]
    [Register(typeof(WindowsService), Scope.SingleInstance, typeof(IWindowServices))]
    [Register(typeof(ConsoleLogService), Scope.SingleInstance, typeof(ILogger))]
    public partial class DIContainer : IContainer<Logic.Startup>
    {
        private readonly string rootPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="DIContainer"/> class.
        /// </summary>
        /// <param name="rootPath">Application root path.</param>
        public DIContainer(string rootPath)
        {
            this.rootPath = rootPath;
        }

        /// <summary>
        /// Gets the path provider.
        /// </summary>
        /// <returns>An IPathProvider.</returns>
        [Factory]
        private IPathProvider GetPathProvider()
        {
            return new PathProvider(this.rootPath);
        }
    }
}
