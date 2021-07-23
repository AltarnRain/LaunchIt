// <copyright file="CommandLineArgumentParserTests.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Tests.Common
{
    using Domain.Models.Parsing;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Tests.Base;

    /// <summary>
    /// Tests the <see cref="CommandLineArgumentParser"/> class.
    /// </summary>
    /// <seealso cref="TestBase" />
    [TestClass]
    public class CommandLineArgumentParserTests : TestBase
    {
        /// <summary>
        /// Parses the test.
        /// </summary>
        [TestMethod]
        public void ParseInitializeTest()
        {
            using (var scope = this.StartTestScope())
            {
                // Arrange
                var target = scope.CommandLineArgumentParser;

                var arguments = new[]
                {
                    Domain.Constants.CommandLineSwitches.Initialize,
                };

                // Act
                var result = target.Parse(arguments);

                // Assert
                Assert.IsTrue(result.Succes);
                Assert.AreEqual(1, result.CommandLineArgumentInfos.Length);
                var commandLineArgumentInfo = result.CommandLineArgumentInfos[0];

                Assert.AreEqual(commandLineArgumentInfo.ArgumentType, CommandLineArgumentType.Initialize);
            }
        }

        /// <summary>
        /// Parses the test.
        /// </summary>
        [TestMethod]
        public void ParseKillExplorerTest()
        {
            using (var scope = this.StartTestScope())
            {
                // Arrange
                var target = scope.CommandLineArgumentParser;

                var arguments = new[]
                {
                    Domain.Constants.CommandLineSwitches.NoExplorer,
                };

                // Act
                var result = target.Parse(arguments);

                // Assert
                Assert.IsTrue(result.Succes);
                Assert.AreEqual(1, result.CommandLineArgumentInfos.Length);
                var commandLineArgumentInfo = result.CommandLineArgumentInfos[0];

                Assert.AreEqual(commandLineArgumentInfo.ArgumentType, CommandLineArgumentType.NoExplorer);
            }
        }

        /// <summary>
        /// Parses the test.
        /// </summary>
        [TestMethod]
        public void ParseRunGameTest()
        {
            using (var scope = this.StartTestScope())
            {
                // Arrange
                var target = scope.CommandLineArgumentParser;

                var arguments = new[]
                {
                    $"{Domain.Constants.CommandLineSwitches.RunGame}=AGame.exe",
                };

                // Act
                var result = target.Parse(arguments);

                // Assert
                Assert.IsTrue(result.Succes);
                Assert.AreEqual(1, result.CommandLineArgumentInfos.Length);
                var commandLineArgumentInfo = result.CommandLineArgumentInfos[0];

                Assert.AreEqual(commandLineArgumentInfo.ArgumentType, CommandLineArgumentType.RunGame);
                Assert.AreEqual("AGame.exe", commandLineArgumentInfo.Value);
            }
        }

        /// <summary>
        /// Parses the test.
        /// </summary>
        [TestMethod]
        public void ParsePriorityTest()
        {
            using (var scope = this.StartTestScope())
            {
                // Arrange
                var target = scope.CommandLineArgumentParser;

                var arguments = new[]
                {
                    $"{Domain.Constants.CommandLineSwitches.Priority}=normal",
                };

                // Act
                var result = target.Parse(arguments);

                // Assert
                Assert.IsTrue(result.Succes);
                Assert.AreEqual(1, result.CommandLineArgumentInfos.Length);
                var commandLineArgumentInfo = result.CommandLineArgumentInfos[0];

                Assert.AreEqual(CommandLineArgumentType.Priority, commandLineArgumentInfo.ArgumentType);
                Assert.IsNotNull("normal", commandLineArgumentInfo.Value);
            }
        }

        /// <summary>
        /// Parses the duplicate argument.
        /// </summary>
        [TestMethod]
        public void ParseDuplicateArgument()
        {
            using (var scope = this.StartTestScope())
            {
                // Arrange
                var target = scope.CommandLineArgumentParser;

                var arguments = new[]
                {
                    $"{Domain.Constants.CommandLineSwitches.Priority}=normal",
                    $"{Domain.Constants.CommandLineSwitches.Priority}=normal",
                };

                // Act
                var result = target.Parse(arguments);

                // Assert
                Assert.IsFalse(result.Succes);
                Assert.AreEqual(1, result.ErrorMessages.Length);
            }
        }

        /// <summary>
        /// Parses the unknown argument.
        /// </summary>
        [TestMethod]
        public void ParseUnknownArgument()
        {
            using (var scope = this.StartTestScope())
            {
                // Arrange
                var target = scope.CommandLineArgumentParser;

                var arguments = new[]
                {
                    $"madeup1",
                };

                // Act
                var result = target.Parse(arguments);

                // Assert
                Assert.IsFalse(result.Succes);
                Assert.AreEqual(1, result.ErrorMessages.Length);
            }
        }
    }
}