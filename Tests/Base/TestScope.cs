﻿// <copyright file="TestScope.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Tests.Base
{
    using global::Infrastructure.Common;
    using global::Infrastructure.Providers;
    using Logic.Common;
    using Logic.Providers;
    using Ninject;
    using System;
    using Tests.TestImplementations;

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
        public FileActionModelProvider FileActionModelProvider => this.kernel.Get<FileActionModelProvider>();

        /// <summary>
        /// Gets the path provider.
        /// </summary>
        public IPathProvider PathProvider => this.kernel.Get<IPathProvider>();

        /// <summary>
        /// Gets the game optimized action handler.
        /// </summary>
        public IGameOptimizerActionHandler GameOptimizerActionHandler => this.kernel.Get<GameOptimizerActionHandler>();

        /// <summary>
        /// Gets the window services.
        /// </summary>
        public TestRunningProgramsHelper TestRunningProgramsHelper => (TestRunningProgramsHelper)this.kernel.Get<IRunningProgramsHelper>();

        /// <summary>
        /// Gets the action file service.
        /// </summary>
        public ActionFileService ActionFileService => this.kernel.Get<ActionFileService>();

        /// <summary>
        /// Gets the command line argument parser.
        /// </summary>
        public ICommandLineArgumentParser CommandLineArgumentParser => this.kernel.Get<ICommandLineArgumentParser>();

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.kernel.Dispose();
        }
    }
}