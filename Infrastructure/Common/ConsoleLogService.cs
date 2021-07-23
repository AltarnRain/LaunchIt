// <copyright file="ConsoleLogService.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Common
{
    using Logic.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Service to log to the console.
    /// </summary>
    /// <seealso cref="Logic.Common.ILogger" />
    public class ConsoleLogService : Logic.Common.ILogger
    {
        /// <summary>
        /// The log cache.
        /// </summary>
        private readonly List<object[]> logCache = new();

        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Log(string message)
        {
            Console.WriteLine(message);
        }

        /// <summary>
        /// Shows the specified headers.
        /// </summary>
        /// <param name="headers">The headers.</param>
        public void Show(string[]? headers = null)
        {
            foreach (var line in this.logCache.FormatTable(headers))
            {
                this.Log(line);
            }
        }

        /// <summary>
        /// Pushes the specified messages.
        /// </summary>
        /// <param name="messages">The messages.</param>
        public void Push(params object[] messages)
        {
            this.logCache.Add(messages);
        }

        /// <summary>
        /// Gets the specified headers.
        /// </summary>
        /// <param name="headers">The headers.</param>
        /// <returns>Get all messages, including headers if provided.</returns>
        public string[] Get(string[]? headers = null)
        {
            return this.logCache.FormatTable(headers);
        }
    }
}
