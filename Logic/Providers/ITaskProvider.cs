// <copyright file="ITaskProvider.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Logic.Providers
{
    using Domain.Models.Task;

    /// <summary>
    /// Defines a contract for a class that provides tasks models.
    /// </summary>
    public interface ITaskProvider
    {
        /// <summary>
        /// Gets the tasks.
        /// </summary>
        /// <returns>
        /// Task model's.
        /// </returns>
        public GameOptimizerActionModel[] GetGameOptimizerActions();
    }
}
