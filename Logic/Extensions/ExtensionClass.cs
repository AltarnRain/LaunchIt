// <copyright file="ExtensionClass.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Logic.Extensions
{
    using Logic.Providers;
    using System.Diagnostics;

    /// <summary>
    /// Class that provides various extension methods.
    /// </summary>
    public static class ExtensionClass
    {
        /// <summary>
        /// Gets the action file path.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <returns>Location of the actions.txt file.</returns>
        public static string ConfigurationFile(this IPathProvider self)
        {
            return self.MapPath("~/GameLauncher.yml");
        }

        /// <summary>
        /// Gets the priority.
        /// </summary>
        /// <param name="priority">The priority.</param>
        /// <returns>The priority class.</returns>
        /// <exception cref="System.ArgumentException">'{nameof(priority)}' cannot be null or whitespace. - priority.</exception>
        /// <exception cref="System.Exception">Unknown priority '{priority}'.</exception>
        public static ProcessPriorityClass GetPriority(this string priority)
        {
            if (string.IsNullOrWhiteSpace(priority))
            {
                throw new System.ArgumentException($"'{nameof(priority)}' cannot be null or whitespace.", nameof(priority));
            }

            return priority.ToUpper() switch
            {
                "IDLE" => ProcessPriorityClass.Idle,
                "BELOWNORMAL" => ProcessPriorityClass.BelowNormal,
                "NORMAL" => ProcessPriorityClass.Normal,
                "ABOVENORMAL" => ProcessPriorityClass.AboveNormal,
                "HIGH" => ProcessPriorityClass.High,
                "REALTIME" => ProcessPriorityClass.RealTime,
                _ => throw new System.Exception($"Unknown priority '{priority}'"),
            };
        }
    }
}
