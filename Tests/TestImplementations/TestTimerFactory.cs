// <copyright file="TestTimerFactory.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Tests.TestImplementations
{
    using Infrastructure.Contracts.DotNETAbstractions;
    using Infrastructure.Contracts.Factories;
    using System.Timers;

    /// <summary>
    /// Test implementation for <see cref="ITimerFactory"/>.
    /// </summary>
    public class TestTimerFactory : ITimerFactory
    {
        /// <summary>
        /// Creates the specified time.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <param name="elapsed">The actions triggered when the timeout elapses.</param>
        /// <returns>
        /// An ITimer object.
        /// </returns>
        public ITimer Create(int time, params ElapsedEventHandler[] elapsed)
        {
            return new TestTimer();
        }
    }
}
