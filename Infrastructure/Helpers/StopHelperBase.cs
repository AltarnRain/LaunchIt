// <copyright file="StopHelperBase.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Helpers
{
    using Logic.Handlers;
    using Logic.Helpers;

    /// <summary>
    /// Base class for classes that stop stuff.
    /// </summary>
    /// <seealso cref="Logic.Helpers.IStopHelper" />
    public abstract class StopHelperBase : IStopHelper
    {
        /// <summary>
        /// The counter.
        /// </summary>
        private readonly Counter counter = new();

        /// <summary>
        /// Stops the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="trackCount">if set to <c>true</c> [track count].</param>
        public abstract void Stop(string name, bool trackCount = true);

        /// <summary>
        /// Gets the stop count.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>Returns the number of times 'name' has been stopped.</returns>
        public int GetStopCount(string name)
        {
            return this.counter.GetCount(name);
        }

        /// <summary>
        /// Adds to stop count.
        /// </summary>
        /// <param name="name">The name.</param>
        public void AddToStopCount(string name)
        {
            this.counter.Add(name);
        }
    }
}