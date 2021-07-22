// <copyright file="TestScope.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Tests.Base
{
    using global::Infrastructure.Providers;
    using Logic.Common;
    using Logic.Providers;
    using Ninject;
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
            this.kernel = new StandardKernel(new TestsBindings(rootPath));
        }

        /// <summary>
        /// Gets the file task provider.
        /// </summary>
        public FileTaskProvider FileTaskProvider => this.kernel.Get<FileTaskProvider>();

        /// <summary>
        /// Gets the path provider.
        /// </summary>
        public IPathProvider PathProvider => this.kernel.Get<IPathProvider>();

        /// <summary>
        /// Gets the game optimized action handler.
        /// </summary>
        public IGameOptimizedActionHandler GameOptimizedActionHandler => this.kernel.Get<IGameOptimizedActionHandler>();

        /// <summary>
        /// Gets the window services.
        /// </summary>
        public IRunningProgramsHelper WindowsServices => this.kernel.Get<IRunningProgramsHelper>();

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.kernel.Dispose();
        }
    }
}