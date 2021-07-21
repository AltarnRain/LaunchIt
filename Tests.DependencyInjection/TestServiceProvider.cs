// <copyright file="TestServiceProvider.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Tests.DependencyInjection
{
    using Infrastructure.Providers;
    using Logic.Providers;

    /// <summary>
    /// Test Scope.
    /// </summary>
    public class TestServiceProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestServiceProvider"/> class.
        /// </summary>
        /// <param name="fileTaskProvider">The file task provider.</param>
        /// <param name="pathProvider">The path provider.</param>
        public TestServiceProvider(FileTaskProvider fileTaskProvider, IPathProvider pathProvider)
        {
            this.FileTaskProvider = fileTaskProvider;
            this.PathProvider = pathProvider;
        }

        /// <summary>
        /// Gets the file task provider.
        /// </summary>
        public FileTaskProvider FileTaskProvider { get; }

        /// <summary>
        /// Gets the path provider.
        /// </summary>
        public IPathProvider PathProvider { get; }
    }
}