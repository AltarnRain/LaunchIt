// <copyright file="ILoggerService.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Logic.Services
{
    using System;

    /// <summary>
    /// Define a contract for a logger.
    /// </summary>
    public interface ILoggerService
    {
        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Log(string message);

        /// <summary>
        /// Logs the skipped.
        /// </summary>
        /// <param name="message">The message.</param>
        public void LogSkipped(string message);

        /// <summary>
        /// Logs the stopped.
        /// </summary>
        /// <param name="message">The message.</param>
        public void LogStopped(string message);

        /// <summary>
        /// Subscribes the specified log action.
        /// </summary>
        /// <param name="logAction">The log action.</param>
        /// <returns>Desubscription action.</returns>
        public Action Subscribe(Action<string> logAction);
    }
}
