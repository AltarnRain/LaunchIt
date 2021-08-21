// <copyright file="TimerFactory.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Factories
{
    using Infrastructure.Contracts.DotNETAbstractions;
    using Infrastructure.Contracts.Factories;
    using System;
    using System.Timers;

    /// <summary>
    /// Returns an <see cref="ITimer"/> object.
    /// </summary>
    /// <seealso cref="Infrastructure.Contracts.Factories.ITimerFactory" />
    public class TimerFactory : ITimerFactory
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
            var timer = new Infrastructure.DotNETAbstractions.Timer(time);
            foreach(var e in elapsed)
            {
                timer.Elapsed += e;
            }

            return timer;
        }
    }
}
