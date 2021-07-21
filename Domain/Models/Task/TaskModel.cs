// <copyright file="TaskModel.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Domain.Models.Task
{
    /// <summary>
    /// Defines a task performed by the application.
    /// </summary>
    public class TaskModel
    {
        /// <summary>
        /// Gets or sets the task action.
        /// </summary>
        public TaskAction TaskAction { get; set; }

        /// <summary>
        /// Gets or sets the task target.
        /// </summary>
        public TaskTarget TaskTarget { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}