// <copyright file="IBatchRunner.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Helpers
{
    using System.Diagnostics;

    /// <summary>
    /// Contract for a batch runner.
    /// </summary>
    public interface IBatchRunner
    {
        /// <summary>
        /// Runs this instance.
        /// </summary>
        /// <returns>A process.</returns>
        Process? Run();
    }
}