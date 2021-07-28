// <copyright file="ConsoleLogger.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Loggers
{
    /// <summary>
    /// Service to log to the console.
    /// </summary>
    public class ConsoleLogger
    {
        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Log(string message)
        {
            System.Console.WriteLine(message);
        }
    }
}
