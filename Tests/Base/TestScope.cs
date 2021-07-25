// <copyright file="TestScope.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Tests.Base
{
    using Infrastructure.Common;
    using Logic.Providers;
    using Ninject;
    using Presentation;
    using System;

    /// <summary>
    /// Test Scope.
    /// </summary>
    public class TestScope : IDisposable
    {
        private readonly IKernel kernel;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestScope" /> class.
        /// </summary>
        /// <param name="rootPath">The root path.</param>
        public TestScope(string rootPath)
        {
            this.kernel = new StandardKernel(new SharedBindings(rootPath));
        }

        /// <summary>
        /// Gets the path provider.
        /// </summary>
        public IPathProvider PathProvider => this.kernel.Get<IPathProvider>();

        /// <summary>
        /// Gets the yaml configuration service.
        /// </summary>
        public ConfigurationService YamlConfigurationService => this.kernel.Get<ConfigurationService>();

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.kernel.Dispose();
        }
    }
}