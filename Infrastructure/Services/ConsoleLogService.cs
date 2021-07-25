// <copyright file="ConsoleLogService.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Services
{
    using System;

    /// <summary>
    /// Service to log to the console.
    /// </summary>
    /// <seealso cref="Logic.Services.ILoggerService" />
    public class ConsoleLogService : Logic.Services.ILoggerService
    {
        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
