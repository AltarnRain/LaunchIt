// <copyright file="ILoggerService.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Logic.Services
{
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
    }
}
