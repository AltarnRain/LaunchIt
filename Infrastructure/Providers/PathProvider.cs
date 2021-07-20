// <copyright file="PathProvider.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Providers
{
    using Logic.Providers;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;

    /// <summary>
    /// Provides paths relative to the application's folders.
    /// </summary>
    /// <seealso cref="Logic.Providers.IPathProvider" />
    public class PathProvider : IPathProvider
    {
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
            var assemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            if (assemblyLocation is null)
            {
                throw new System.Exception("Could not find the execution directory of the executable");
            }

            var returnValue = Path.Combine(assemblyLocation, strippedPath);

            return returnValue;
        }
    }
}
