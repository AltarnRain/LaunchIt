// <copyright file="IActionModelProvider.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Logic.Providers
{
    using Domain.Models.Action;

    /// <summary>
    /// Defines a contract for a class that provides tasks models.
    /// </summary>
    public interface IActionModelProvider
    {
        /// <summary>
        /// Gets the actions.
        /// </summary>
        /// <returns>
        /// Task model's.
        /// </returns>
        public GameOptimizerActionModel[] GetActions();
    }
}
