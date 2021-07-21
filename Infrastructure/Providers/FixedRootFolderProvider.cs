// <copyright file="FixedRootFolderProvider.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Providers
{
    /// <summary>
    /// Used while debugging. Returns the solution folder.
    /// </summary>
    /// <seealso cref="Logic.Providers.IRootFolderProvider" />
    public class FixedRootFolderProvider : Logic.Providers.IRootFolderProvider
    {
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>
        /// The root folder of the application.
        /// </returns>
        public string? Get()
        {
            return @"C:\Reps\GameLauncher";
        }
    }
}
