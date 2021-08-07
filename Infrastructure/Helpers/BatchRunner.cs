// <copyright file="BatchRunner.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Helpers
{
    using Logic.Contracts.Services;
    using System.Diagnostics;
    using System.IO;

    /// <summary>
    /// Executes a batch file.
    /// </summary>
    public class BatchRunner : IBatchRunner
    {
        private readonly string batchContent;
        private readonly ILogEventService logger;
        private readonly IProcessWrapper processWrapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="BatchRunner" /> class.
        /// </summary>
        /// <param name="batchContent">Content of the batch.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="processWrapper">The process wrapper.</param>
        public BatchRunner(string batchContent, ILogEventService logger, IProcessWrapper processWrapper)
        {
            this.batchContent = batchContent;
            this.logger = logger;
            this.processWrapper = processWrapper;
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
            var process = this.processWrapper.Start(tempCmdFile);

            return process;
        }
    }
}
