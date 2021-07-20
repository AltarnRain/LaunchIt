// <copyright file="IUnstoppableServiceProvider.cs" company="Onno Invernizzi">
// Copyright (c) Onno Invernizzi. All rights reserved.
// </copyright>

namespace Logic.Providers
{
    /// <summary>
    /// Contract for a class that provides services that cannot be stopped.
    /// </summary>
    public interface IUnstoppableServiceProvider
    {
        /// <summary>
        /// Gets the unstoppable services.
        /// </summary>
        /// <returns>Unstoppable services.</returns>
        string[] GetUnstoppableServices();
    }
}
