// <copyright file="TestLogger.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Tests.TestImplementations
{
    using Logic.Contracts.Loggers;
    using System.Collections.Generic;

    /// <summary>
    /// Logger for testing.
    /// </summary>
    /// <seealso cref="Logic.Contracts.Loggers.ILog" />
    public class TestLogger : ILog
    {
        /// <summary>
        /// Gets or sets the messages.
        /// </summary>
        public List<string> Messages { get; set; } = new();

        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Log(string message)
        {
            this.Messages.Add(message);
        }
    }
}
