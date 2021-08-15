// <copyright file="ExtensionClassTests.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Extensions.Tests
{
    using Domain.Models.Configuration;
    using Logic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests the <see cref="Infrastructure.Extensions.ExtensionClass"/>.
    /// </summary>
    [TestClass]
    public class ExtensionClassTests
    {
        /// <summary>
        /// Gets the configuration file test.
        /// </summary>
        [TestMethod]
        public void GetDefaultConfigurationFileTest()
        {
            // Arrange
            var commandLineArguments = new CommandLineArgument[]
            {
                new CommandLineArgument
                {
                },
            };

            // Act
            var configFile = commandLineArguments.GetConfigurationFile();

            // Assert
            Assert.IsTrue(configFile.Contains(Domain.Constants.ConfigurationConstants.DefaultConfigurationFileName));
        }

        /// <summary>
        /// Gets the configuration file test.
        /// </summary>
        [TestMethod]
        public void GetCustomConfigurationFileTest()
        {
            // Arrange
            var commandLineArguments = new CommandLineArgument[]
            {
                new CommandLineArgument
                {
                    Command = SwitchCommands.Config.ToString(),
                    Options = new[]
                    {
                        "myProfile",
                    },
                },
            };

            // Act
            var configFile = commandLineArguments.GetConfigurationFile();

            // Assert
            Assert.IsTrue(configFile.Contains("myProfile.yml"));
        }
    }
}