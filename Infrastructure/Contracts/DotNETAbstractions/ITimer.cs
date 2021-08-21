// <copyright file="ITimer.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Contracts.DotNETAbstractions
{
    using System;
    using System.Timers;

    /// <summary>
    /// Abstraction for the <see cref="System.Timers.Timer" />.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface ITimer : IDisposable
    {
        /// <summary>
        /// Occurs when [elapsed].
        /// </summary>
        event ElapsedEventHandler Elapsed;

        /// <summary>
        /// Starts this instance.
        /// </summary>
        void Start();

        /// <summary>
        /// Stops this instance.
        /// </summary>
        void Stop();

        /// <summary>
        /// Closes this instance.
        /// </summary>
        void Close();
    }
}
