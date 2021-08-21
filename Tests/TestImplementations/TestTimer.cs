// <copyright file="TestTimer.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Tests.TestImplementations
{
    using Infrastructure.Contracts.DotNETAbstractions;
    using System;
    using System.Timers;

    /// <summary>
    /// Test implementation fo the  timer.
    /// </summary>
    /// <seealso cref="Infrastructure.Contracts.DotNETAbstractions.ITimer" />
    public class TestTimer : ITimer
    {
        /// <summary>
        /// Occurs when [elapsed].
        /// </summary>
        public event ElapsedEventHandler Elapsed;

        /// <summary>
        /// Closes this instance.
        /// </summary>
        public void Close()
        {
            // Does nothing.
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // Does nothing.
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            // Does nothing.
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            // Does nothing.
        }
    }
}
