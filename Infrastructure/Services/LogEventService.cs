// <copyright file="LogEventService.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Services
{
    using Logic.Contracts.Loggers;
    using Logic.Contracts.Services;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Service to log to the console.
    /// </summary>
    /// <seealso cref="Logic.Contracts.Services.ILogEventService" />
    public class LogEventService : ILogEventService
    {
        private readonly List<ILog> subscriptions = new();

        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Log(string message)
        {
            var timestamp = DateTime.Now.ToString("yyyymmdd-HHmm");
            var messageToLog = $"{timestamp} {message}";

            // Inform subscribers.
            this.subscriptions.ForEach(s => s.Log(messageToLog));
        }

        /// <summary>
        /// Logs the skipped.
        /// </summary>
        /// <param name="message">The message.</param>
        public void LogSkipped(string message)
        {
            this.Log($"Skipped: {message}");
        }

        /// <summary>
        /// Logs the stopped.
        /// </summary>
        /// <param name="message">The message.</param>
        public void LogStopped(string message)
        {
            this.Log($"Stopped: {message}");
        }

        /// <summary>
        /// Subscribes the specified log action.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <returns>
        /// Desubscribe action.
        /// </returns>
        public Action Subscribe(ILog logger)
        {
            this.subscriptions.Add(logger);

            return () => this.subscriptions.Remove(logger);
        }
    }
}
