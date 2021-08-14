// <copyright file="LaunchModelUpdaterTests.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Parsers.Tests
{
    using Domain.Models.Configuration;
    using Logic;
    using Logic.Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests <see cref="LaunchModelUpdater.Parse(string[])"/>.
    /// </summary>
    [TestClass]
    public class LaunchModelUpdaterTests
    {
        /// <summary>
        /// Parses the edit argument.
        /// </summary>
        [TestMethod]
        public void ParseEditTest()
        {
            // Arrange
            var model = new LaunchModel();

            // Act
            LaunchModelUpdater.UpdateWithCommandLineArguments(new[] { SwitchCommands.Edit.GetCommandLineArgument() }, model);

            // Assert
            Assert.IsTrue(model.EditConfiguration);
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
            LaunchModelUpdater.UpdateWithCommandLineArguments(new[] { SwitchCommands.UseBatch.GetCommandLineArgument() }, model);

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
            LaunchModelUpdater.UpdateWithCommandLineArguments(new[] { SwitchCommands.ShutdownExplorer.GetCommandLineArgument() }, model);

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
            LaunchModelUpdater.UpdateWithCommandLineArguments(new[] { $"{SwitchCommands.Priority.GetCommandLineArgument()}=idle" }, model);
            Assert.AreEqual(System.Diagnostics.ProcessPriorityClass.Idle, model.Priority);

            LaunchModelUpdater.UpdateWithCommandLineArguments(new[] { $"{SwitchCommands.Priority.GetCommandLineArgument()}=belownormal" }, model);
            Assert.AreEqual(System.Diagnostics.ProcessPriorityClass.BelowNormal, model.Priority);

            LaunchModelUpdater.UpdateWithCommandLineArguments(new[] { $"{SwitchCommands.Priority.GetCommandLineArgument()}=normal" }, model);
            Assert.AreEqual(System.Diagnostics.ProcessPriorityClass.Normal, model.Priority);

            LaunchModelUpdater.UpdateWithCommandLineArguments(new[] { $"{SwitchCommands.Priority.GetCommandLineArgument()}=abovenormal" }, model);
            Assert.AreEqual(System.Diagnostics.ProcessPriorityClass.AboveNormal, model.Priority);

            LaunchModelUpdater.UpdateWithCommandLineArguments(new[] { $"{SwitchCommands.Priority.GetCommandLineArgument()}=high" }, model);
            Assert.AreEqual(System.Diagnostics.ProcessPriorityClass.High, model.Priority);

            LaunchModelUpdater.UpdateWithCommandLineArguments(new[] { $"{SwitchCommands.Priority.GetCommandLineArgument()}=realtime" }, model);
            Assert.AreEqual(System.Diagnostics.ProcessPriorityClass.RealTime, model.Priority);
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
                    $"{SwitchCommands.Services.GetCommandLineArgument()}=Service 1,Service 2",
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
                Executables = new[]
                {
                    "Executable A",
                    "Executable B",
                },
            };

            // Act
            LaunchModelUpdater.UpdateWithCommandLineArguments(
                new[]
                {
                    $"{SwitchCommands.Executables.GetCommandLineArgument()}=Executable 1,Executable 2",
                }, model);

            // Assert
            Assert.AreEqual(4, model.Executables.Length);
            Assert.AreEqual("Executable A", model.Executables[0]);
            Assert.AreEqual("Executable B", model.Executables[1]);
            Assert.AreEqual("Executable 1", model.Executables[2]);
            Assert.AreEqual("Executable 2", model.Executables[3]);
        }

        /// <summary>
        /// Parses the nothing test.
        /// </summary>
        [TestMethod]
        public void ParseNothingTest()
        {
            // Arrange
            var model = new LaunchModel();

            // Act
            LaunchModelUpdater.UpdateWithCommandLineArguments(System.Array.Empty<string>(), model);
        }

        /// <summary>
        /// Parses the nothing test.
        /// </summary>
        [TestMethod]
        public void ParseNonsense()
        {
            // Arrange
            var model = new LaunchModel();

            // Act
            LaunchModelUpdater.UpdateWithCommandLineArguments(new string[] { "-nope" }, model);
        }

        /// <summary>
        /// Parses the nothing test.
        /// </summary>
        [TestMethod]
        public void ParseNoServiceSpecified()
        {
            // Arrange
            var model = new LaunchModel();

            // Act
            LaunchModelUpdater.UpdateWithCommandLineArguments(new string[] { SwitchCommands.Services.GetCommandLineArgument()}, model);

            // Assert
            Assert.AreEqual(0, model.Services.Length);
        }

        /// <summary>
        /// Parses the nothing test.
        /// </summary>
        [TestMethod]
        public void ParseNoExecutableSpecified()
        {
            // Arrange
            var model = new LaunchModel();

            // Act
            LaunchModelUpdater.UpdateWithCommandLineArguments(new string[] { SwitchCommands.Services.GetCommandLineArgument() }, model);

            // Assert
            Assert.AreEqual(0, model.Executables.Length);
        }

        /// <summary>
        /// Parses the nothing test.
        /// </summary>
        [TestMethod]
        public void ParseRun()
        {
            // Arrange
            var model = new LaunchModel();

            // Act
            LaunchModelUpdater.UpdateWithCommandLineArguments(new string[] { $"{SwitchCommands.Run.GetCommandLineArgument()}=notepad.exe" }, model);

            // Assert
            Assert.AreEqual("notepad.exe", model.ExecutableToLaunch);
        }

        /// <summary>
        /// Parses the nothing test.
        /// </summary>
        [TestMethod]
        public void TestInvalidOptionsThrow()
        {
            // Arrange
            var model = new LaunchModel();

            // Act & Assert
            Assert.ThrowsException<System.NotSupportedException>(() => LaunchModelUpdater.UpdateWithCommandLineArguments(new string[] { SwitchCommands.Run.GetCommandLineArgument() }, model));
            Assert.ThrowsException<System.NotSupportedException>(() => LaunchModelUpdater.UpdateWithCommandLineArguments(new string[] { "-priority=high,low" }, model));
        }
    }
}