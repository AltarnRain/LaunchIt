// <copyright file="DIContainer.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Tests.DependencyInjection
{
    using global::Infrastructure.Providers;
    using Logic.Providers;
    using StrongInject;

    /// <summary>
    /// DI Container for unit tests.
    /// </summary>
    /// <seealso cref="StrongInject.IContainer{Tests.TestScope}" />
    [Register(typeof(TestScope), Scope.SingleInstance)]
    [Register(typeof(FileTaskProvider), Scope.SingleInstance)]
    public partial class DIContainer : IContainer<TestScope>
    {
        private readonly string rootFolder;

        private DIContainer(string rootFolder)
        {
            this.rootFolder = rootFolder;
        }

        /// <summary>
        /// Gets the test scope.
        /// </summary>
        /// <param name="rootFolder">The root folder.</param>
        /// <returns>A Service provider for unit tests.</returns>
        public static TestScope GetScope(string rootFolder)
        {
            var container = new DIContainer(rootFolder);
            return container.Resolve().Value;
        }

        [Factory]
        private IPathProvider GetPathProvider()
        {
            return new PathProvider(this.rootFolder);
        }
    }
}
