// <copyright file="ConfigurationServiceTests.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Services.Tests
{
    using Domain.Models.Configuration;
    using global::Tests.Base;
    using Infrastructure.Services;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.IO;

    /// <summary>
    /// Tests the <see cref="ConfigurationService"/>.
    /// </summary>
    /// <seealso cref="TestBase" />
    [TestClass]
    public class ConfigurationServiceTests : TestBase
    {
        /// <summary>
        /// Cleans the configuration file.
        /// </summary>
        [TestInitialize]
        public void CleanConfigFile()
        {
            using var scope = this.StartTestScope();
            var configurationFile = scope.GetTestConfigurationFileName();

            if (File.Exists(configurationFile))
            {
                File.Delete(configurationFile);
            }
        }

        /// <summary>
        /// Writes the test.
        /// </summary>
        [TestMethod]
        public void WriteTest()
        {
            // Arrange
            using var scope = this.StartTestScope(typeof(ConfigurationService));
            var target = scope.Get<ConfigurationService>();
            var file = scope.GetTestConfigurationFileName();

            var configurationModel = new ConfigurationModel
            {
                Services = new[]
                {
                    "Service a",
                    "Service b",
                },
                Executables = new[]
                {
                    "Executable a",
                    "Executable b",
                },
            };

            // Act
            target.Write(configurationModel);

            // Assert
            Assert.IsTrue(File.Exists(file));
        }

        /// <summary>
        /// Configurations the file exists test.
        /// </summary>
        [TestMethod]
        public void ConfigurationFileExistsTest()
        {
            // Arrange
            using var scope = this.StartTestScope(typeof(ConfigurationService));
            var target = scope.Get<ConfigurationService>();

            var configurationFile = scope.GetTestConfigurationFileName();

            // Act
            var result1 = target.ConfigurationFileExists(); // No file

            File.WriteAllText(configurationFile, "Make file");

            // Act
            var result2 = target.ConfigurationFileExists();

            // Assert
            Assert.IsFalse(result1);
            Assert.IsTrue(result2);
        }

        /// <summary>
        /// Tests the write example configuration file.
        /// </summary>
        [TestMethod]
        public void TestWriteExampleConfigurationFile()
        {
            // Arrange
            using var scope = this.StartTestScope(typeof(ConfigurationService));
            var target = scope.Get<ConfigurationService>();
            var configurationFile = scope.GetTestConfigurationFileName();

            // Act
            target.WriteExampleConfigurationFile();

            // Assert
            Assert.IsTrue(File.Exists(configurationFile));

            var fileContent = File.ReadAllText(configurationFile);

            // Check if example information was written. The example config file is the default settings with
            // examples how to configure services and executabled.
            Assert.IsTrue(fileContent.Contains("Example executable 1"));
        }

        /// <summary>
        /// Reads the no configuration file.
        /// </summary>
        [TestMethod]
        public void ReadNoConfigFile()
        {
            // Arrange
            using var scope = this.StartTestScope(typeof(ConfigurationService));
            var target = scope.Get<ConfigurationService>();

            // Act
            var result = target.Read();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Services.Length);
            Assert.AreEqual(0, result.Executables.Length);
        }

        /// <summary>
        /// Reads the no configuration file.
        /// </summary>
        [TestMethod]
        public void ReadConfigFile()
        {
            // Arrange
            using var scope = this.StartTestScope(typeof(ConfigurationService));
            var target = scope.Get<ConfigurationService>();

            var configurationModel = new ConfigurationModel
            {
                Services = new[]
                {
                    "A",
                    "B",
                },
            };

            target.Write(configurationModel);

            // Act
            var result = target.Read();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Services.Length);
            Assert.AreEqual("A", result.Services[0]);
            Assert.AreEqual("B", result.Services[1]);
        }

        /// <summary>
        /// Tests reading an invalid configuration file.
        /// </summary>
        [TestMethod]
        public void InvalidConfigurationFileTest()
        {
            // Arrange
            using var scope = this.StartTestScope(typeof(ConfigurationService));
            var target = scope.Get<ConfigurationService>();

            var configurationFile = scope.GetTestConfigurationFileName();
            File.WriteAllText(configurationFile, "This is not valid");

            // Act
            var result = target.Read();

            // Assert
            Assert.IsNotNull(result);
        }
    }
}