// <copyright file="GameOptimizerActionModel.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Domain.Models.Action
{
    /// <summary>
    /// Defines a action performed by the application.
    /// </summary>
    public class GameOptimizerActionModel
    {
        /// <summary>
        /// Gets or sets the task target.
        /// </summary>
        public ActionTarget TaskTarget { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}