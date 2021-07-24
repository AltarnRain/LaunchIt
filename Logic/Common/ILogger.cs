// <copyright file="ILogger.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Logic.Common
{
    /// <summary>
    /// Define a contract for a logger.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Log(string message);
    }
}
