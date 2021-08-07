// <copyright file="BatchRunnerTests.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Helpers.Tests
{
    using global::Tests.Base;
    using Infrastructure.Helpers;
    using Logic.Contracts.Services;
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
            var logEventService = scope.Get<ILogEventService>();
            var processWrapper = scope.Get<IProcessWrapper>();

            var fileToRemove = Path.GetTempFileName();
            File.WriteAllText(fileToRemove, "Some content");

            var batchRunner = new BatchRunner("Something cool", logEventService, processWrapper);

            // Act
            batchRunner.Run();

            // Assert
            var testProcessWrapper = scope.GetTestProcessWrapper();

            Assert.AreEqual(1, testProcessWrapper.StartCalls);
        }
    }
}