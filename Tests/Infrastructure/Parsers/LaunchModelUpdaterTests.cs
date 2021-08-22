// <copyright file="LaunchModelUpdaterTests.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Parsers.Tests
{
    using Domain.Models.Configuration;
    using Logic;
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

            var arguments = new CommandLineArgument[]
            {
                new CommandLineArgument
                {
                    Command = SwitchCommands.Edit.ToString(),
                },
            };

            // Act
            LaunchModelUpdater.UpdateWithCommandLineArguments(arguments, model);

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

            var arguments = new CommandLineArgument[]
            {
                new CommandLineArgument
                {
                    Command = SwitchCommands.UseBatch.ToString(),
                },
            };

            // Act
            LaunchModelUpdater.UpdateWithCommandLineArguments(arguments, model);

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

            var arguments = new CommandLineArgument[]
            {
                new CommandLineArgument
                {
                    Command = SwitchCommands.ShutdownExplorer.ToString(),
                },
            };

            // Act
            LaunchModelUpdater.UpdateWithCommandLineArguments(arguments, model);

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

            var commandLineArgument = new CommandLineArgument
            {
                Command = SwitchCommands.Priority.ToString(),
                Options = new string[1],
            };

            var commandLineArguments = new CommandLineArgument[]
            {
                commandLineArgument,
            };

            // Assert
            commandLineArgument.Options[0] = "idle";
            LaunchModelUpdater.UpdateWithCommandLineArguments(commandLineArguments, model);
            Assert.AreEqual(System.Diagnostics.ProcessPriorityClass.Idle, model.Priority);

            commandLineArgument.Options[0] = "belownormal";
            LaunchModelUpdater.UpdateWithCommandLineArguments(commandLineArguments, model);
            Assert.AreEqual(System.Diagnostics.ProcessPriorityClass.BelowNormal, model.Priority);

            commandLineArgument.Options[0] = "normal";
            LaunchModelUpdater.UpdateWithCommandLineArguments(commandLineArguments, model);
            Assert.AreEqual(System.Diagnostics.ProcessPriorityClass.Normal, model.Priority);

            commandLineArgument.Options[0] = "abovenormal";
            LaunchModelUpdater.UpdateWithCommandLineArguments(commandLineArguments, model);
            Assert.AreEqual(System.Diagnostics.ProcessPriorityClass.AboveNormal, model.Priority);

            commandLineArgument.Options[0] = "high";
            LaunchModelUpdater.UpdateWithCommandLineArguments(commandLineArguments, model);
            Assert.AreEqual(System.Diagnostics.ProcessPriorityClass.High, model.Priority);

            commandLineArgument.Options[0] = "realtime";
            LaunchModelUpdater.UpdateWithCommandLineArguments(commandLineArguments, model);
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

            var commandLineArguments = new CommandLineArgument[]
            {
                new CommandLineArgument
                {
                    Command = SwitchCommands.Services.ToString(),
                    Options = new[] { "Service 1", "Service 2" },
                },
            };

            // Act
            LaunchModelUpdater.UpdateWithCommandLineArguments(commandLineArguments, model);

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

            var commandLineArguments = new CommandLineArgument[]
            {
                new CommandLineArgument
                {
                    Command = SwitchCommands.Executables.ToString(),
                    Options = new[]
                    {
                        "Executable 1",
                        "Executable 2",
                    },
                },
            };

            // Act
            LaunchModelUpdater.UpdateWithCommandLineArguments(commandLineArguments, model);

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

            var commandLineArguments = System.Array.Empty<CommandLineArgument>();

            // Act
            LaunchModelUpdater.UpdateWithCommandLineArguments(commandLineArguments, model);
        }

        /// <summary>
        /// Parses the nothing test.
        /// </summary>
        [TestMethod]
        public void ParseNonsense()
        {
            // Arrange
            var model = new LaunchModel();

            var commandLineArguments = new CommandLineArgument[]
            {
                new CommandLineArgument
                {
                    Command = "nope",
                },
            };

            // Act
            LaunchModelUpdater.UpdateWithCommandLineArguments(commandLineArguments, model);
        }

        /// <summary>
        /// Parses the nothing test.
        /// </summary>
        [TestMethod]
        public void ParseNoServiceSpecified()
        {
            // Arrange
            var model = new LaunchModel();

            var commandLineArguments = new CommandLineArgument[]
            {
                new CommandLineArgument
                {
                    Command = SwitchCommands.Services.ToString(),
                },
            };

            // Act
            LaunchModelUpdater.UpdateWithCommandLineArguments(commandLineArguments, model);

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

            var commandLineArguments = new CommandLineArgument[]
            {
                new CommandLineArgument
                {
                    Command = SwitchCommands.Services.ToString(),
                },
            };

            // Act
            LaunchModelUpdater.UpdateWithCommandLineArguments(commandLineArguments, model);

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

            var commandLineArguments = new CommandLineArgument[]
            {
                new CommandLineArgument
                {
                    Command = SwitchCommands.Run.ToString(),
                    Options = new[] { "notepad.exe" },
                },
            };

            // Act
            LaunchModelUpdater.UpdateWithCommandLineArguments(commandLineArguments, model);

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

            var invalidRun = new CommandLineArgument[]
            {
                new CommandLineArgument
                {
                    Command = SwitchCommands.Run.ToString(),
                },
            };

            var invalidPriority = new CommandLineArgument[]
            {
                new CommandLineArgument
                {
                    Command = SwitchCommands.Priority.ToString(),
                },
            };

            // Act & Assert
            Assert.ThrowsException<System.NotSupportedException>(() => LaunchModelUpdater.UpdateWithCommandLineArguments(invalidRun, model));
            Assert.ThrowsException<System.NotSupportedException>(() => LaunchModelUpdater.UpdateWithCommandLineArguments(invalidPriority, model));
        }
    }
}