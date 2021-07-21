// <copyright file="IRootFolderProvider.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Logic.Providers
{
    /// <summary>
    /// Returns the root of the application.
    /// </summary>
    public interface IRootFolderProvider
    {
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>The root folder of the application.</returns>
        public string? Get();
    }
}