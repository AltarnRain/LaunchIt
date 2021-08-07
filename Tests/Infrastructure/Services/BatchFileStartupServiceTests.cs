// <copyright file="BatchFileStartupServiceTests.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Services.Tests
{
    using Domain.Models.Configuration;
    using global::Tests.Base;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Linq;

    /// <summary>
    /// Tests <see cref="BatchFileStartupService"/>.
    /// </summary>
    [TestClass]
    public class BatchFileStartupServiceTests : TestBase
    {
        /// <summary>
        /// Starts the test.
        /// </summary>
        [TestMethod]
        public void StartNoExecutableTest()
        {
            // Arrange
            var scope = this.StartTestScope(typeof(BatchFileStartupService));
            var target = scope.Get<BatchFileStartupService>();

            var launchModel = new LaunchModel
            {
                Executables = new[]
                {
                    "A",
                    "B",
                },
                Services = new[]
                {
                    "1",
                    "2",
                },
                ShutdownExplorer = true,
            };

            // Act
            var result = target.Start(launchModel);

            // Assert
            Assert.IsNull(result); // Nothing to run.
        }

        /// <summary>
        /// Starts the test.
        /// </summary>
        [TestMethod]
        public void StartTest()
        {
            // Arrange
            var scope = this.StartTestScope(typeof(BatchFileStartupService));
            var target = scope.Get<BatchFileStartupService>();

            var testBatchFileRunnerFactory = scope.GetTestBatchRunnerFactory();

            var launchModel = new LaunchModel
            {
                Executables = new[]
                {
                    "A",
                    "B",
                },
                Services = new[]
                {
                    "1",
                    "2",
                },
                ShutdownExplorer = true,
                ExecutableToLaunch = "Z:\\NoSuchFolder\\dummy.exe",
                Priority = System.Diagnostics.ProcessPriorityClass.RealTime,
            };

            // Act
            target.Start(launchModel);

            // Assert
            var testBatchRunner = testBatchFileRunnerFactory.TestBatchRunner;

            Assert.IsNotNull(testBatchRunner);

            var runCalls = testBatchRunner.RunCalls;
            var ranContent = testBatchRunner.BatchFileContent;

            Assert.AreEqual(1, runCalls);
            Assert.IsNotNull(ranContent);

            Assert.IsTrue(ranContent.Contains("taskkill /f /im explorer.exe")); // shut down explorer

            Assert.IsTrue(ranContent.Contains("net stop \"1\"")); // shut down service 1
            Assert.IsTrue(ranContent.Contains("net stop \"2\"")); // shut down service 1

            Assert.IsTrue(ranContent.Contains("taskkill /f /im \"A\"")); // shut down service A
            Assert.IsTrue(ranContent.Contains("taskkill /f /im \"B\"")); // shut down service B

            Assert.IsTrue(ranContent.Contains("cd /d \"Z:\\NoSuchFolder\"")); // Dummy.exe is in a folder.

            Assert.IsTrue(ranContent.Contains("@pause")); // Wait for started application to end.

            // return $"start \"{executableName}\" /{priorityClass.ToString().ToUpper()} {executableName}";
            Assert.IsTrue(ranContent.Contains("start \"dummy.exe\" /REALTIME \"dummy.exe\""));
        }

        /// <summary>
        /// Starts the test.
        /// </summary>
        [TestMethod]
        public void StartNoFolderWarningTest()
        {
            // Arrange
            var scope = this.StartTestScope(typeof(BatchFileStartupService));
            var target = scope.Get<BatchFileStartupService>();

            var testBatchFileRunnerFactory = scope.GetTestBatchRunnerFactory();

            var launchModel = new LaunchModel
            {
                ExecutableToLaunch = "dummy.exe",
            };

            // Act
            target.Start(launchModel);

            // Assert
            var testBatchRunner = testBatchFileRunnerFactory.TestBatchRunner;

            Assert.IsNotNull(testBatchRunner);

            var runCalls = testBatchRunner.RunCalls;
            Assert.AreEqual(1, runCalls);

            var logs = scope.GetLogs();

            Assert.IsTrue(logs.Any(l => l.Contains("Looks like you didn't specify a folder. No worries, I'll try to start dummy.exe")));
        }
    }
}