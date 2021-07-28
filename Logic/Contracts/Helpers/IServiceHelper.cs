// <copyright file="IServiceHelper.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Logic.Contracts.Helpers
{
    /// <summary>
    /// Contract for a service helper.
    /// </summary>
    public interface IServiceHelper : IStopHelper
    {
        /// <summary>
        /// Gets the running services.
        /// </summary>
        /// <returns>List is service names that have status 'Running'.</returns>
        string[] GetRunningServices();
    }
}
