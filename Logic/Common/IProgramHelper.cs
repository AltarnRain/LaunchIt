// <copyright file="IProgramHelper.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Logic.Common
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Contract for a Program Helper that shutsdown processes.
    /// </summary>
    public interface IProgramHelper
    {
        /// <summary>
        /// Stops a running process.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>A Task.</returns>
        IEnumerable<Task> Stop(string name);
    }
}
