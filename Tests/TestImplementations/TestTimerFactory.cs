// <copyright file="TestTimerFactory.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Tests.TestImplementations
{
    using Infrastructure.Contracts.DotNETAbstractions;
    using Infrastructure.Contracts.Factories;
    using System;
    using System.Collections.Generic;
    using System.Timers;

    /// <summary>
    /// Test implementation for <see cref="ITimerFactory"/>.
    /// </summary>
    public class TestTimerFactory : ITimerFactory
    {
        /// <summary>
        /// Gets the test timers.
        /// </summary>
        public List<TestTimer> TestTimers { get; private set; } = new();

        /// <summary>
        /// Gets the events.
        /// </summary>
        public ElapsedEventHandler[] Events { get; private set; } = Array.Empty<ElapsedEventHandler>();

        /// <summary>
        /// Creates the specified time.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <param name="events">The actions triggered when the timeout elapses.</param>
        /// <returns>
        /// An ITimer object.
        /// </returns>
        public ITimer Create(int time, params ElapsedEventHandler[] events)
        {
            var testTimer = new TestTimer();
            this.TestTimers.Add(testTimer);

            this.Events = events;

            return testTimer;
        }
    }
}
