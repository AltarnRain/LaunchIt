// <copyright file="PathProvider.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Providers
{
    using Logic.Contracts.Providers;
    using System.IO;

    /// <summary>
    /// Provides paths relative to the application's folders.
    /// </summary>
    /// <seealso cref="Logic.Contracts.Providers.IPathProvider" />
    public class PathProvider : IPathProvider
    {
        private readonly string rootPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="PathProvider" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="rootPath">The root.</param>
        public PathProvider(string rootPath)
        {
            this.rootPath = rootPath;
        }

        /// <summary>
        /// Maps the path.
        /// </summary>
        /// <param name="relativePath">The relative path.</param>
        /// <returns>Path relative to the executing assembly.</returns>
        /// <exception cref="System.Exception">
        /// Please pass a relative path prefixed with '~'. Use '~/' to get the root directoty.
        /// or
        /// Please pass a relative path prefixed with '~'. Use '~/' to get the root directoty.
        /// </exception>
        public string MapPath(string relativePath)
        {
            if (!relativePath.StartsWith("~"))
            {
                throw new System.Exception("Please pass a relative path prefixed with '~'. Use '~/' to get the root directoty.");
            }

            var strippedPath = relativePath[2..];

            var returnValue = Path.Combine(this.rootPath, strippedPath);

            return returnValue;
        }
    }
}
