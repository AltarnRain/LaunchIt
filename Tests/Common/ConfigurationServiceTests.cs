// <copyright file="ConfigurationServiceTests.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Common.Tests
{
    using Domain.Models.Configuration;
    using global::Tests.Base;
    using Logic.Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.IO;

    /// <summary>
    /// Tests the <see cref="ConfigurationService"/>.
    /// </summary>
    /// <seealso cref="Tests.Base.TestBase" />
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