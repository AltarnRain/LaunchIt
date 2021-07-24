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
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
