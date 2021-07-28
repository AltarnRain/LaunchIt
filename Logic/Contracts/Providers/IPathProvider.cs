// <copyright file="IPathProvider.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Logic.Contracts.Providers
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
