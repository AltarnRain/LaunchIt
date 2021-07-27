// <copyright file="Counter.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Logic.Handlers
{
    using System.Collections.Generic;

    /// <summary>
    /// Helper class to keep track of the number of times .Add was called for a certain 'item'.
    /// </summary>
    public class Counter
    {
        private readonly Dictionary<string, int> registry = new();

        /// <summary>
        /// Stoppeds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Add count.</returns>
        public int Add(string item)
        {
            if (this.registry.ContainsKey(item))
            {
                this.registry[item]++;
                return this.registry[item];
            }

            // First time. Set to 1.
            this.registry.Add(item, 1);
            return 1;
        }

        /// <summary>
        /// Counts the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Count value.</returns>
        public int Count(string item)
        {
            if (this.registry.ContainsKey(item))
            {
                return this.registry[item];
            }

            return 0;
        }
    }
}
