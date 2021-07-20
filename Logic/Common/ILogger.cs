// <copyright file="ILogger.cs" company="Onno Invernizzi">
// Copyright (c) Onno Invernizzi. All rights reserved.
// </copyright>

namespace Logic.Common
{
    /// <summary>
    /// Define a contract for a logger.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Pushes the specified messages.
        /// </summary>
        /// <param name="messages">The messages.</param>
        public void Push(params object[] messages);

        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Log(string message);

        /// <summary>
        /// Shows the specified headers.
        /// </summary>
        /// <param name="headers">The headers.</param>
        public void Show(params string[]? headers);

        /// <summary>
        /// Gets the current log.
        /// </summary>
        /// <param name="headers">The headers.</param>
        /// <returns>Log lines.</returns>
        public string[] Get(params string[]? headers);
    }
}
