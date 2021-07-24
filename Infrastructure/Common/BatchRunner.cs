// <copyright file="BatchRunner.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Common
{
    using System.Diagnostics;
    using System.IO;

    /// <summary>
    /// Executes a batch file.
    /// </summary>
    public class BatchRunner
    {
        private readonly string batchContent;

        /// <summary>
        /// Initializes a new instance of the <see cref="BatchRunner" /> class.
        /// </summary>
        /// <param name="batchContent">Content of the batch.</param>
        public BatchRunner(string batchContent)
        {
            this.batchContent = batchContent;
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        /// <returns>Runs the batch file and returns the process reference.</returns>
        public Process? Run()
        {
            var tempCmdFile = Path.GetTempFileName() + ".cmd";
            File.WriteAllText(tempCmdFile, this.batchContent);

            var processStartInfo = new ProcessStartInfo
            {
                UseShellExecute = true,
                FileName = tempCmdFile,
            };

            var process = Process.Start(processStartInfo);

            return process;
        }
    }
}
