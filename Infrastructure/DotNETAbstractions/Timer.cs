// <copyright file="Timer.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.DotNETAbstractions
{
    using Infrastructure.Contracts.DotNETAbstractions;

    /// <summary>
    /// .NET Timer abstraction so we can use our own interface. Also means we do not need to wrap the timer class in our own class.
    /// </summary>
    /// <seealso cref="System.Timers.Timer" />
    /// <seealso cref="Infrastructure.Contracts.DotNETAbstractions.ITimer" />
    public class Timer : System.Timers.Timer, ITimer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Timer"/> class.
        /// </summary>
        /// <param name="time">The time.</param>
        public Timer(int time)
            : base(time)
        {
        }
    }
}
