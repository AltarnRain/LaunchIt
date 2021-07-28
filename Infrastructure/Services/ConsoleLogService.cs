// <copyright file="ConsoleLogService.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Services
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Service to log to the console.
    /// </summary>
    /// <seealso cref="Logic.Services.ILoggerService" />
    public class ConsoleLogService : Logic.Services.ILoggerService
    {
        private readonly List<Action<string>> subscriptions = new();

        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Log(string message)
        {
            var timestamp = DateTime.Now.ToString("yyyymmdd-HHmm");
            var messageToLog = $"{timestamp} {message}";

            System.Console.WriteLine(messageToLog);

            // Inform subscribers.
            this.subscriptions.ForEach(s => s(messageToLog));
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
        /// <param name="logAction">The log action.</param>
        /// <returns>Desubscribe action.</returns>
        public Action Subscribe(Action<string> logAction)
        {
            this.subscriptions.Add(logAction);

            return () => this.subscriptions.Remove(logAction);
        }
    }
}
