// <copyright file="IBatchRunnerFactory.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Factories
{
    using Infrastructure.Helpers;

    /// <summary>
    /// Contract for a factory class that returns an <see cref="IBatchRunner"/>.
    /// </summary>
    public interface IBatchRunnerFactory
    {
        /// <summary>
        /// Creates the specified batch content.
        /// </summary>
        /// <param name="batchContent">Content of the batch.</param>
        /// <returns>An IBatchRunner.</returns>
        IBatchRunner Create(string batchContent);
    }
}