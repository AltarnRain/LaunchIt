// <copyright file="ConfigurationServiceTests.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Tests.Services
{
    using Domain.Models.Configuration;
    using Infrastructure.Services;
    using Logic.Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.IO;
    using Tests.Base;

    /// <summary>
    /// Tests the <see cref="ConfigurationService"/>.
    /// </summary>
    /// <seealso cref="TestBase" />
    [TestClass]
    public class ConfigurationServiceTests : TestBase
    {
        /// <summary>
        /// Reads the test.
        /// </summary>
        [TestMethod]
        public void ReadTest()
        {
            using var scope = this.StartTestScope();
            var target = scope.ConfigurationService;
        }

        /// <summary>
        /// Writes the test.
        /// </summary>
        [TestMethod]
        public void WriteTest()
        {
            // Arrange
            using var scope = this.StartTestScope();
            var target = scope.ConfigurationService;

            var file = scope.PathProvider.ConfigurationFile();

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
    }
}