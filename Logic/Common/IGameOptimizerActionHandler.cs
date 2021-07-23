// <copyright file="IGameOptimizerActionHandler.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Logic.Common
{
    using Domain.Models.Action;

    /// <summary>
    /// Defines a contract for a class that handles a <see cref="GameOptimizerActionModel"/>.
    /// </summary>
    public interface IGameOptimizerActionHandler
    {
        /// <summary>
        /// Resolves the specified action.
        /// </summary>
        /// <param name="action">The action.</param>
        void Handle(GameOptimizerActionModel action);
    }
}
