// <copyright file="CommandLineParserTests.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Parsers.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests <see cref="CommandLineParser.Parse(string[])"/>.
    /// </summary>
    [TestClass]
    public class CommandLineParserTests
    {
        /// <summary>
        /// Parses the reset argument.
        /// </summary>
        [TestMethod]
        public void ParseResetTest()
        {
            // Act
            var result = CommandLineParser.Parse(new[] { "-reset" });

            // Assert
            Assert.IsTrue(result.ResetConfiguration);
        }

        /// <summary>
        /// Parses the edit argument.
        /// </summary>
        [TestMethod]
        public void ParseEditTest()
        {
            // Act
            var result = CommandLineParser.Parse(new[] { "-edit" });

            // Assert
            Assert.IsTrue(result.EditConfiguration);
        }

        /// <summary>
        /// Parses the 'cmd' argument.
        /// </summary>
        [TestMethod]
        public void ParseExecutableTest()
        {
            // Act
            var result = CommandLineParser.Parse(new[] { "cmd" });

            // Assert
            Assert.IsNotNull(result.LaunchModel.ExecutableToLaunch);
        }

        /// <summary>
        /// Parse -usebatch.
        /// </summary>
        [TestMethod]
        public void UseBatchTest()
        {
            // Act
            var result = CommandLineParser.Parse(new[] { "-usebatch" });

            // Assert
            Assert.IsTrue(result.LaunchModel.UseBatchFile);
        }

        /// <summary>
        /// Parses -shutdownexplorer.
        /// </summary>
        [TestMethod]
        public void ShutdownExplorerTest()
        {
            // Act
            var result = CommandLineParser.Parse(new[] { "-shutdownexplorer" });

            // Assert
            Assert.IsTrue(result.LaunchModel.ShutdownExplorer);
        }

        /// <summary>
        /// Tests all priority command line switches.
        /// </summary>
        [TestMethod]
        public void PriorityTest()
        {
            // Assert
            Assert.AreEqual(System.Diagnostics.ProcessPriorityClass.Idle, CommandLineParser.Parse(new[] { "-priority", "idle" }).LaunchModel.Priority);
            Assert.AreEqual(System.Diagnostics.ProcessPriorityClass.BelowNormal, CommandLineParser.Parse(new[] { "-priority", "belownormal" }).LaunchModel.Priority);
            Assert.AreEqual(System.Diagnostics.ProcessPriorityClass.Normal, CommandLineParser.Parse(new[] { "-priority", "normal" }).LaunchModel.Priority);
            Assert.AreEqual(System.Diagnostics.ProcessPriorityClass.AboveNormal, CommandLineParser.Parse(new[] { "-priority", "abovenormal" }).LaunchModel.Priority);
            Assert.AreEqual(System.Diagnostics.ProcessPriorityClass.High, CommandLineParser.Parse(new[] { "-priority", "high" }).LaunchModel.Priority);
            Assert.AreEqual(System.Diagnostics.ProcessPriorityClass.RealTime, CommandLineParser.Parse(new[] { "-priority", "realtime" }).LaunchModel.Priority);
        }

        /// <summary>
        /// Parses the monitor restarts.
        /// </summary>
        [TestMethod]
        public void ParseMonitorRestarts()
        {
            // Act
            var result1 = CommandLineParser.Parse(new[] { "-monitorrestarts", "true" });
            var result2 = CommandLineParser.Parse(new[] { "-monitorrestarts", "false" });

            // Assert
            Assert.IsTrue(result1.LaunchModel.MonitoringConfiguration.MonitorRestarts);
            Assert.IsFalse(result2.LaunchModel.MonitoringConfiguration.MonitorRestarts);
        }

        /// <summary>
        /// Parses the MonitoringInterval.
        /// </summary>
        [TestMethod]
        public void ParseMonitorInterval()
        {
            // Act
            var result1 = CommandLineParser.Parse(new[] { "-monitorinterval", "180" });

            // Assert
            Assert.AreEqual(180, result1.LaunchModel.MonitoringConfiguration.MonitoringInterval);
        }

        /// <summary>
        /// Parses ServiceShutdown commands.
        /// </summary>
        [TestMethod]
        public void ParseServiceShutdownCommands()
        {
            // Act
            var result = CommandLineParser.Parse(
                new[]
                {
                    "-ServiceShutdownAfterRestart",
                    "false",
                    "-ServiceShutdownOnlyConfigured",
                    "false",
                    "-ServiceShutdownMaximumAttempts",
                    "5",
                });

            // Assert
            Assert.IsFalse(result.LaunchModel.ServiceShutdownConfiguration.ShutdownAfterRestart);
            Assert.IsFalse(result.LaunchModel.ServiceShutdownConfiguration.OnlyConfigured);
            Assert.AreEqual(5, result.LaunchModel.ServiceShutdownConfiguration.MaximumShutdownAttempts);
        }

        /// <summary>
        /// Parses ServiceShutdown commands.
        /// </summary>
        [TestMethod]
        public void ParseExecutableShutdownCommands()
        {
            // Act
            var result = CommandLineParser.Parse(
                new[]
                {
                    "-ExecutableShutdownAfterRestart",
                    "false",
                    "-ExecutableShutdownOnlyConfigured",
                    "false",
                    "-ExecutableShutdownMaximumAttempts",
                    "5",
                });

            // Assert
            Assert.IsFalse(result.LaunchModel.ExecutableShutdownConfiguration.ShutdownAfterRestart);
            Assert.IsFalse(result.LaunchModel.ExecutableShutdownConfiguration.OnlyConfigured);
            Assert.AreEqual(5, result.LaunchModel.ExecutableShutdownConfiguration.MaximumShutdownAttempts);
        }

        /// <summary>
        /// Parses the shutdown service.
        /// </summary>
        [TestMethod]
        public void ParseShutdownService()
        {
            // Act
            var result = CommandLineParser.Parse(
                new[]
                {
                    "-ShutdownService",
                    "Service 1",
                    "-ShutdownService",
                    "Service 2",
                });

            // Assert
            Assert.AreEqual(2, result.LaunchModel.Services.Length);
            Assert.AreEqual("Service 1", result.LaunchModel.Services[0]);
            Assert.AreEqual("Service 2", result.LaunchModel.Services[1]);
        }

        /// <summary>
        /// Parses the shutdown service.
        /// </summary>
        [TestMethod]
        public void ParseShutdownExecutable()
        {
            // Act
            var result = CommandLineParser.Parse(
                new[]
                {
                    "-ShutdownExecutable",
                    "Executable 1",
                    "-ShutdownExecutable",
                    "Executable 2",
                });

            // Assert
            Assert.AreEqual(2, result.LaunchModel.Executables.Length);
            Assert.AreEqual("Executable 1", result.LaunchModel.Executables[0]);
            Assert.AreEqual("Executable 2", result.LaunchModel.Executables[1]);
        }
    }
}