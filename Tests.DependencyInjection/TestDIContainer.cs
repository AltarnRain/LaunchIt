// <copyright file="TestDIContainer.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Tests.DependencyInjection
{
    using Infrastructure.Providers;
    using StrongInject;

    /// <summary>
    /// DI Container for unit tests.
    /// </summary>
    /// <seealso cref="StrongInject.IContainer{Tests.TestScope}" />
    [Register(typeof(TestServiceProvider), Scope.SingleInstance)]
    [Register(typeof(FileTaskProvider), Scope.SingleInstance)]
    public partial class TestDIContainer : IContainer<TestServiceProvider>
    {
        private readonly string rootFolder;

        private TestDIContainer(string rootFolder)
        {
            this.rootFolder = rootFolder;
        }

        /// <summary>
        /// Gets the test service provider.
        /// </summary>
        /// <param name="rootFolder">The root folder.</param>
        /// <returns>A Service provider for unit tests.</returns>
        public static TestServiceProvider GetTestServiceProvider(string rootFolder)
        {
            var container = new TestDIContainer(rootFolder);
            return container.Resolve().Value;
        }

        [Factory]
        private Logic.Providers.IPathProvider GetPathProvider()
        {
            return new PathProvider(this.rootFolder);
        }
    }
}
