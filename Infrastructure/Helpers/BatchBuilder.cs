// <copyright file="BatchBuilder.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Helpers
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Class to build a batch file.
    /// </summary>
    public class BatchBuilder
    {
        private readonly List<string> lines = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="BatchBuilder"/> class.
        /// </summary>
        public BatchBuilder()
        {
            this.lines.Add("@echo off");
            this.lines.Add("@cls");
        }

        /// <summary>
        /// Adds the specified line.
        /// </summary>
        /// <param name="line">The line.</param>
        public void Add(string line)
        {
            this.lines.Add(line);
        }

        /// <summary>
        /// Echoes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Echo(string message)
        {
            this.lines.Add($"@echo {message}");
        }

        /// <summary>
        /// Rems the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Rem(string message)
        {
            this.lines.Add($"@rem {message}");
        }

        /// <summary>
        /// Pauses this instance.
        /// </summary>
        public void Pause()
        {
            this.lines.Add("@pause");
        }

        /// <summary>
        /// Adds the empty line.
        /// </summary>
        public void AddEmptyLine()
        {
            this.lines.Add(string.Empty);
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// Batch file as a string.
        /// </returns>
        public override string ToString()
        {
            var content = string.Join(Environment.NewLine, this.lines);
            return content;
        }

        /// <summary>
        /// Lines at.
        /// </summary>
        /// <param name="index">The line index.</param>
        /// <returns>Line at the specified index.</returns>
        public string LineAt(int index)
        {
            return this.lines[index];
        }
    }
}
