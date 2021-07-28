// <copyright file="FileLogger.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Helpers
{
    using Logic.Loggers;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Logs to a file.
    /// </summary>
    public class FileLogger : ILog
    {
        private readonly List<string> logCache = new();
        private string? fileName;

        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        public string FileName
        {
            get
            {
                if (this.fileName is null)
                {
                    this.fileName = Path.GetTempFileName() + ".txt";
                }

                return this.fileName;
            }
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
            using (var fw = System.IO.File.AppendText(this.FileName))
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
