// <copyright file="TestBatchRunner.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Tests.TestImplementations
{
    using Infrastructure.Helpers;
    using System.Diagnostics;

    /// <summary>
    /// Test implementation of a <see cref="IBatchRunner"/>.
    /// </summary>
    /// <seealso cref="Infrastructure.Helpers.IBatchRunner" />
    public class TestBatchRunner : IBatchRunner
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestBatchRunner"/> class.
        /// </summary>
        /// <param name="batchFileContent">Content of the batch file.</param>
        public TestBatchRunner(string batchFileContent)
        {
            this.BatchFileContent = batchFileContent;
        }

        /// <summary>
        /// Gets the run calls.
        /// </summary>
        public int RunCalls { get; private set; }

        /// <summary>
        /// Gets the content of the batch file.
        /// </summary>
        /// <value>
        /// The content of the batch file.
        /// </value>
        public string BatchFileContent { get; private set; }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        /// <returns>
        /// A process.
        /// </returns>
        public Process? Run()
        {
            this.RunCalls++;
            return null;
        }
    }
}
