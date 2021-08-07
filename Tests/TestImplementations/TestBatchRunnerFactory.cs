// <copyright file="TestBatchRunnerFactory.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Tests.TestImplementations
{
    using Infrastructure.Factories;
    using Infrastructure.Helpers;

    /// <summary>
    /// Test implementation for <see cref="IBatchRunnerFactory"/>.
    /// </summary>
    /// <seealso cref="Infrastructure.Factories.IBatchRunnerFactory" />
    public class TestBatchRunnerFactory : IBatchRunnerFactory
    {
        /// <summary>
        /// Gets the test batch runner.
        /// </summary>
        public TestBatchRunner? TestBatchRunner { get; private set; }

        /// <summary>
        /// Creates the specified batch content.
        /// </summary>
        /// <param name="batchFileContent">Content of the batch.</param>
        /// <returns>An IBatchRunner.</returns>
        public IBatchRunner Create(string batchFileContent)
        {
            this.TestBatchRunner = new TestBatchRunner(batchFileContent);
            return this.TestBatchRunner;
        }
    }
}
