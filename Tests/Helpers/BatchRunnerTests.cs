// <copyright file="BatchRunnerTests.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Helpers.Tests
{
    using global::Tests.Base;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.IO;

    /// <summary>
    /// Tests <see cref="BatchRunner"/>.
    /// </summary>
    [TestClass]
    public class BatchRunnerTests : TestBase
    {
        /// <summary>
        /// Runs the test.
        /// </summary>
        [TestMethod]
        public void RunTest()
        {
            // Arrange
            using var scope = this.StartTestScope();

            var fileToRemove = Path.GetTempFileName();
            System.IO.File.WriteAllText(fileToRemove, "Some content");

            var batchCommand = $"del /s /q {fileToRemove}";
            var batchRunner = new BatchRunner(batchCommand, scope.LogEventService);

            var fileToRemoveExistsBeforeBatchRunnerRunCommand = System.IO.File.Exists(fileToRemove);

            // Act
            var process = batchRunner.Run();
            process?.WaitForExit();

            // Assert
            Assert.IsTrue(fileToRemoveExistsBeforeBatchRunnerRunCommand);

            var fileToRemoveExistsAfterBatchRunnerRunCommand = System.IO.File.Exists(fileToRemove);

            Assert.IsFalse(fileToRemoveExistsAfterBatchRunnerRunCommand);
        }
    }
}