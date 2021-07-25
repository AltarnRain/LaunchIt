// <copyright file="IMemoryCleanupService.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Logic
{
    /// <summary>
    /// Contract for a class that cleans up memory.
    /// </summary>
    public interface IMemoryCleanupService
    {
        /// <summary>
        /// Does the cleanup.
        /// </summary>
        void Cleanup();
    }
}