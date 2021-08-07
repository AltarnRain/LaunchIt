// <copyright file="MonitoringException.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Domain.Exceptions
{
    /// <summary>
    /// Exception thrown when an error occurs while monitoring.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class MonitoringException : System.Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MonitoringException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public MonitoringException(string message)
            : base(message)
        {
        }
    }
}
