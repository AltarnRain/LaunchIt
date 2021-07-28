// <copyright file="ILog.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Logic.Contracts.Loggers
{
    /// <summary>
    /// Contract for a class that has a log method.
    /// </summary>
    public interface ILog
    {
        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Log(string message);
    }
}