// <copyright file="BatchRunner.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Helpers
{
    using Logic.Services;
    using System.Diagnostics;
    using System.IO;

    /// <summary>
    /// Executes a batch file.
    /// </summary>
    public class BatchRunner
    {
        private readonly string batchContent;
        private readonly ILogEventService logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="BatchRunner" /> class.
        /// </summary>
        /// <param name="batchContent">Content of the batch.</param>
        /// <param name="logger">The logger.</param>
        public BatchRunner(string batchContent, ILogEventService logger)
        {
            this.batchContent = batchContent;
            this.logger = logger;
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        /// <returns>Runs the batch file and returns the process reference.</returns>
        public Process? Run()
        {
            var tempCmdFile = Path.GetTempFileName() + ".cmd";

            this.logger.Log($"Creating batchfile: {tempCmdFile}");

            File.WriteAllText(tempCmdFile, this.batchContent);

            this.logger.Log($"Starting {tempCmdFile}");
            var process = ProcessWrapper.Start(tempCmdFile);

            return process;
        }
    }
}
