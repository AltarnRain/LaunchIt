// <copyright file="TestScope.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Tests.DependencyInjection
{
    using global::Infrastructure.Providers;
    using Logic.Providers;

    /// <summary>
    /// Test Scope.
    /// </summary>
    public class TestScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestScope"/> class.
        /// </summary>
        /// <param name="fileTaskProvider">The file task provider.</param>
        /// <param name="pathProvider">The path provider.</param>
        public TestScope(FileTaskProvider fileTaskProvider, IPathProvider pathProvider)
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