// <copyright file="CommandLineParserTests.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Parsers.Tests
{
    using Domain.Models.Configuration;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests <see cref="LaunchModelUpdater.Parse(string[])"/>.
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
            // Arrange
            var model = new LaunchModel();

            // Act
            LaunchModelUpdater.UpdateWithCommandLineArguments(new[] { "-reset" }, model);

            // Assert
            Assert.IsTrue(model.ResetConfiguration);
        }

        /// <summary>
        /// Parses the edit argument.
        /// </summary>
        [TestMethod]
        public void ParseEditTest()
        {
            // Arrange
            var model = new LaunchModel();

            // Act
            LaunchModelUpdater.UpdateWithCommandLineArguments(new[] { "-edit" }, model);

            // Assert
            Assert.IsTrue(model.EditConfiguration);
        }

        /// <summary>
        /// Parses the 'cmd' argument.
        /// </summary>
        [TestMethod]
        public void ParseExecutableTest()
        {
            // Arrange
            var model = new LaunchModel() { ExecutableToLaunch = "Something else" };

            // Act
            LaunchModelUpdater.UpdateWithCommandLineArguments(new[] { "cmd" }, model);

            // Assert
            Assert.AreEqual("cmd", model.ExecutableToLaunch);
        }

        /// <summary>
        /// Parse -usebatch.
        /// </summary>
        [TestMethod]
        public void UseBatchTest()
        {
            // Arrange
            var model = new LaunchModel
            {
                UseBatchFile = false,
            };

            // Act
            LaunchModelUpdater.UpdateWithCommandLineArguments(new[] { "-usebatch" }, model);

            // Assert
            Assert.IsTrue(model.UseBatchFile);
        }

        /// <summary>
        /// Parses -shutdownexplorer.
        /// </summary>
        [TestMethod]
        public void ShutdownExplorerTest()
        {
            // Arrange
            var model = new LaunchModel
            {
                ShutdownExplorer = false,
            };

            // Act
            LaunchModelUpdater.UpdateWithCommandLineArguments(new[] { "-shutdownexplorer" }, model);

            // Assert
            Assert.IsTrue(model.ShutdownExplorer);
        }

        /// <summary>
        /// Tests all priority command line switches.
        /// </summary>
        [TestMethod]
        public void PriorityTest()
        {
            // Arrange
            var model = new LaunchModel();

            // Assert
            LaunchModelUpdater.UpdateWithCommandLineArguments(new[] { "-priority", "idle" }, model);
            Assert.AreEqual(System.Diagnostics.ProcessPriorityClass.Idle, model.Priority);

            LaunchModelUpdater.UpdateWithCommandLineArguments(new[] { "-priority", "belownormal" }, model);
            Assert.AreEqual(System.Diagnostics.ProcessPriorityClass.BelowNormal, model.Priority);

            LaunchModelUpdater.UpdateWithCommandLineArguments(new[] { "-priority", "normal" }, model);
            Assert.AreEqual(System.Diagnostics.ProcessPriorityClass.Normal, model.Priority);

            LaunchModelUpdater.UpdateWithCommandLineArguments(new[] { "-priority", "abovenormal" }, model);
            Assert.AreEqual(System.Diagnostics.ProcessPriorityClass.AboveNormal, model.Priority);

            LaunchModelUpdater.UpdateWithCommandLineArguments(new[] { "-priority", "high" }, model);
            Assert.AreEqual(System.Diagnostics.ProcessPriorityClass.High, model.Priority);

            LaunchModelUpdater.UpdateWithCommandLineArguments(new[] { "-priority", "realtime" }, model);
            Assert.AreEqual(System.Diagnostics.ProcessPriorityClass.RealTime, model.Priority);
        }

        /// <summary>
        /// Parses the monitor restarts.
        /// </summary>
        [TestMethod]
        public void ParseMonitorRestarts()
        {
            // Arrange
            var model = new LaunchModel
            {
                MonitoringConfiguration = new MonitoringConfiguration
                {
                    MonitorRestarts = false,
                },
            };

            // Act
            LaunchModelUpdater.UpdateWithCommandLineArguments(new[] { "-monitorrestarts", "true" }, model);

            // Assert
            Assert.IsTrue(model.MonitoringConfiguration.MonitorRestarts);
        }

        /// <summary>
        /// Parses the MonitoringInterval.
        /// </summary>
        [TestMethod]
        public void ParseMonitorInterval()
        {
            // Arrange
            var model = new LaunchModel
            {
                MonitoringConfiguration = new MonitoringConfiguration
                {
                    MonitoringInterval = 300,
                },
            };

            // Act
            LaunchModelUpdater.UpdateWithCommandLineArguments(new[] { "-monitorinterval", "180" }, model);

            // Assert
            Assert.AreEqual(180, model.MonitoringConfiguration.MonitoringInterval);
        }

        /// <summary>
        /// Parses ServiceShutdown commands.
        /// </summary>
        [TestMethod]
        public void ParseServiceShutdownCommands()
        {
            // Arrange
            var model = new LaunchModel
            {
                ServiceShutdownConfiguration = new ShutdownConfigurationModel
                {
                    ShutdownAfterRestart = true,
                    MaximumShutdownAttempts = 3,
                    OnlyConfigured = true,
                },
            };

            // Act
            LaunchModelUpdater.UpdateWithCommandLineArguments(
                new[]
                {
                    "-ServiceShutdownAfterRestart",
                    "false",
                    "-ServiceShutdownOnlyConfigured",
                    "false",
                    "-ServiceShutdownMaximumAttempts",
                    "5",
                }, model);

            // Assert
            Assert.IsFalse(model.ServiceShutdownConfiguration.ShutdownAfterRestart);
            Assert.IsFalse(model.ServiceShutdownConfiguration.OnlyConfigured);
            Assert.AreEqual(5, model.ServiceShutdownConfiguration.MaximumShutdownAttempts);
        }

        /// <summary>
        /// Parses ServiceShutdown commands.
        /// </summary>
        [TestMethod]
        public void ParseExecutableShutdownCommands()
        {
            // Arrange
            var model = new LaunchModel
            {
                ExecutableShutdownConfiguration = new ShutdownConfigurationModel
                {
                    ShutdownAfterRestart = true,
                    MaximumShutdownAttempts = 3,
                    OnlyConfigured = true,
                },
            };

            // Act
            LaunchModelUpdater.UpdateWithCommandLineArguments(
                new[]
                {
                    "-ExecutableShutdownAfterRestart",
                    "false",
                    "-ExecutableShutdownOnlyConfigured",
                    "false",
                    "-ExecutableShutdownMaximumAttempts",
                    "5",
                }, model);

            // Assert
            Assert.IsFalse(model.ExecutableShutdownConfiguration.ShutdownAfterRestart);
            Assert.IsFalse(model.ExecutableShutdownConfiguration.OnlyConfigured);
            Assert.AreEqual(5, model.ExecutableShutdownConfiguration.MaximumShutdownAttempts);
        }

        /// <summary>
        /// Parses the shutdown service.
        /// </summary>
        [TestMethod]
        public void ParseShutdownService()
        {
            var model = new LaunchModel
            {
                Services = new[]
                {
                    "Service A",
                    "Service B",
                },
            };

            // Act
            LaunchModelUpdater.UpdateWithCommandLineArguments(
                new[]
                {
                    "-ShutdownService",
                    "Service 1",
                    "-ShutdownService",
                    "Service 2",
                }, model);

            // Assert
            Assert.AreEqual(4, model.Services.Length);
            Assert.AreEqual("Service A", model.Services[0]);
            Assert.AreEqual("Service B", model.Services[1]);
            Assert.AreEqual("Service 1", model.Services[2]);
            Assert.AreEqual("Service 2", model.Services[3]);
        }

        /// <summary>
        /// Parses the shutdown service.
        /// </summary>
        [TestMethod]
        public void ParseShutdownExecutable()
        {
            var model = new LaunchModel
            {
                Services = new[]
                {
                    "Executable A",
                    "Executable B",
                },
            };

            // Act
            LaunchModelUpdater.UpdateWithCommandLineArguments(
                new[]
                {
                    "-ShutdownExecutable",
                    "Executable 1",
                    "-ShutdownExecutable",
                    "Executable 2",
                }, model);

            // Assert
            Assert.AreEqual(4, model.Executables.Length);
            Assert.AreEqual("Executable A", model.Executables[0]);
            Assert.AreEqual("Executable B", model.Executables[1]);
            Assert.AreEqual("Executable 1", model.Executables[2]);
            Assert.AreEqual("Executable 2", model.Services[3]);
        }
    }
}