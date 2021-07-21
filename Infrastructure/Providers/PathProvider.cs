// <copyright file="PathProvider.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Providers
{
    using Logic.Common;
    using Logic.Providers;
    using System.IO;
    using System.Reflection;

    /// <summary>
    /// Provides paths relative to the application's folders.
    /// </summary>
    /// <seealso cref="Logic.Providers.IPathProvider" />
    public class PathProvider : IPathProvider
    {
        private readonly string root;

        /// <summary>
        /// Initializes a new instance of the <see cref="PathProvider" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="root">The root.</param>
        public PathProvider(string root)
        {
            this.root = root;
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

            var location = this.root ?? Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            if (location is null)
            {
                throw new System.Exception("Could not find the execution directory of the executable");
            }

            var returnValue = Path.Combine(location, strippedPath);

            return returnValue;
        }
    }
}
