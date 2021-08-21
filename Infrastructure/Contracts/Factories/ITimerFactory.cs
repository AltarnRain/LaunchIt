// <copyright file="ITimerFactory.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Contracts.Factories
{
    using Infrastructure.Contracts.DotNETAbstractions;
    using System;
    using System.Timers;

    /// <summary>
    /// Contract for a class that provides an ITimer. Abstracted for unit testing.
    /// </summary>
    public interface ITimerFactory
    {
        /// <summary>
        /// Creates the specified time.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <param name="elapsed">The actions triggered when the timeout elapses.</param>
        /// <returns>
        /// An ITimer object.
        /// </returns>
        public ITimer Create(int time, params ElapsedEventHandler[] elapsed);
    }
}
