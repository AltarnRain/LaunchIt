// <copyright file="AssemblyFolderProvider.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Providers
{
    using System.IO;
    using System.Reflection;

    /// <summary>
    /// Provides the folder of the executing assembly's location.
    /// </summary>
    /// <seealso cref="Logic.Providers.IRootFolderProvider" />
    public class AssemblyFolderProvider : Logic.Providers.IRootFolderProvider
    {
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>
        /// The root folder of the application.
        /// </returns>
        public string? Get()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }
    }
}
