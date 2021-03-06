// <copyright file="FileLogger.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Loggers
{
    using Logic.Contracts.Loggers;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Logs to a file.
    /// </summary>
    public class FileLogger : ILog
    {
        private readonly List<string> logCache = new();
        private readonly int logCacheSize;
        private string? fileName;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileLogger"/> class.
        /// </summary>
        /// <param name="logCacheSize">Size of the log cache.</param>
        public FileLogger(int logCacheSize = 10)
        {
            this.logCacheSize = logCacheSize;
        }

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

            if (this.logCache.Count >= this.logCacheSize)
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
            using (var fw = File.AppendText(this.FileName))
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
