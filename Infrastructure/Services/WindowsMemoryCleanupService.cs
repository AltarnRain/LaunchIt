// <copyright file="WindowsMemoryCleanupService.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Services
{
    using Logic;
    using System.Runtime.Versioning;

    /// <summary>
    /// Class that cleans up memory for Windows.
    /// </summary>
    /// <seealso cref="Logic.IMemoryCleanupService" />
    [SupportedOSPlatform("windows")]
    public class WindowsMemoryCleanupService : IMemoryCleanupService
    {
        /// <summary>
        /// Does the cleanup.
        /// </summary>
        public void Cleanup()
        {
            // Do nothing.
        }
    }
}
