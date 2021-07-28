// <copyright file="IStopHelper.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Logic.Contracts.Helpers
{
    /// <summary>
    /// Contract for a class that helps stop something.
    /// </summary>
    public interface IStopHelper
    {
        /// <summary>
        /// Stops the count.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>Number of times 'name' has been stopped.</returns>
        int GetStopCount(string name);

        /// <summary>
        /// Stops the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="tackCount">if set to <c>true</c> [tack count].</param>
        void Stop(string name, bool tackCount = false);
    }
}
