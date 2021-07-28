// <copyright file="ConsoleLogger.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Loggers
{
    using Logic.Loggers;

    /// <summary>
    /// Service to log to the console.
    /// </summary>
    public class ConsoleLogger : ILog
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
