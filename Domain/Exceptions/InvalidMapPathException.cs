// <copyright file="InvalidMapPathException.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Domain.Exceptions
{
    /// <summary>
    /// Thrown when MapPath is called without the proper prefix.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class InvalidMapPathException : System.Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidMapPathException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidMapPathException(string message)
            : base(message)
        {
        }
    }
}
