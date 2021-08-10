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
        /// Stops the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        void Stop(string name);
    }
}
