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

            var timestampedMessage = $"{timestamp} {message}";
            System.Console.WriteLine(timestampedMessage);

            // Inform subscribers.
            this.subscriptions.ForEach(s => s(timestampedMessage));
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
