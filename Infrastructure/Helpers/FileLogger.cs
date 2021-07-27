// <copyright file="FileLogger.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Helpers
{
    using System.Collections.Generic;

    /// <summary>
    /// Logs to a file.
    /// </summary>
    public class FileLogger
    {
        private readonly string fileName;
        private readonly List<string> logCache = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="FileLogger"/> class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public FileLogger(string fileName)
        {
            this.fileName = fileName;
        }

        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Log(string message)
        {
            this.logCache.Add(message);

            if (this.logCache.Count > 10)
            {
                this.WriteToFile();
            }
        }

        /// <summary>
        /// Closes this instance.
        /// </summary>
        public void Close()
        {
            this.WriteToFile();
        }

        private void WriteToFile()
        {
            using (var fw = System.IO.File.AppendText(this.fileName))
            {
                foreach (var log in this.logCache)
                {
                    fw.WriteLine(log);
                }

                fw.Flush();
                fw.Close();
            }

            this.logCache.Clear();
        }
    }
}
