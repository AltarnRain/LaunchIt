// <copyright file="TestScope.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Tests.Base
{
    using Infrastructure.Helpers;
    using Infrastructure.Services;
    using Logic.Contracts.Providers;
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
        public ConfigurationService ConfigurationService => this.kernel.Get<ConfigurationService>();

        /// <summary>
        /// Gets the windows service helper.
        /// </summary>
        public WindowsServiceHelper WindowsServiceHelper => this.kernel.Get<WindowsServiceHelper>();

        /// <summary>
        /// Gets the windows process helper.
        /// </summary>
        public WindowsProcessHelper WindowsProcessHelper => this.kernel.Get<WindowsProcessHelper>();

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.kernel.Dispose();
        }
    }
}