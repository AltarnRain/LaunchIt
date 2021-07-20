// <copyright file="IPathProvider.cs" company="Onno Invernizzi">
// Copyright (c) Onno Invernizzi. All rights reserved.
// </copyright>

namespace Logic.Providers
{
    /// <summary>
    /// Contrat for a path provider.
    /// </summary>
    public interface IPathProvider
    {
        /// <summary>
        /// Maps the path.
        /// </summary>
        /// <param name="relativePath">The relative path.</param>
        /// <returns>Path relative to the executing assembly.</returns>
        string MapPath(string relativePath);
    }
}
