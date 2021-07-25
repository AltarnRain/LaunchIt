// <copyright file="BatchBuilder.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Common
{
    using System.Text;

    /// <summary>
    /// Class to build a batch file.
    /// </summary>
    public class BatchBuilder
    {
        private readonly StringBuilder stringBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="BatchBuilder"/> class.
        /// </summary>
        public BatchBuilder()
        {
            this.stringBuilder = new StringBuilder();
            this.stringBuilder.AppendLine("@echo off");
            this.stringBuilder.AppendLine("@cls");
        }

        /// <summary>
        /// Adds the specified line.
        /// </summary>
        /// <param name="line">The line.</param>
        public void Add(string line)
        {
            this.stringBuilder.AppendLine(line);
        }

        /// <summary>
        /// Echoes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Echo(string message)
        {
            this.stringBuilder.AppendLine($"@echo {message}");
        }

        /// <summary>
        /// Rems the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Rem(string message)
        {
            this.stringBuilder.AppendLine($"@rem {message}");
        }

        /// <summary>
        /// Pauses this instance.
        /// </summary>
        public void Pause()
        {
            this.stringBuilder.AppendLine("@pause");
        }

        /// <summary>
        /// Adds the empty line.
        /// </summary>
        public void AddEmptyLine()
        {
            this.stringBuilder.AppendLine(string.Empty);
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// Batch file as a string.
        /// </returns>
        public override string ToString()
        {
            return this.stringBuilder.ToString();
        }
    }
}
