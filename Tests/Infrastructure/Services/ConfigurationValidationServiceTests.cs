// <copyright file="ConfigurationValidationServiceTests.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Services.Tests
{
    using Domain.Models.Configuration;
    using global::Tests.Base;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Linq;

    /// <summary>
    /// Tests <see cref="ConfigurationValidationService"/>.
    /// </summary>
    [TestClass]
    public class ConfigurationValidationServiceTests : TestBase
    {
        /// <summary>
        /// Validates the test.
        /// </summary>
        [TestMethod]
        public void ValidateDefaultConfigurationModelTest()
        {
            // Arrange
            var scope = this.StartTestScope(typeof(ConfigurationValidationService));
            var target = scope.Get<ConfigurationValidationService>();

            var configurationModel = new ConfigurationModel();

            // Act
            var result = target.Validate(configurationModel).ToArray();

            // Assert
            Assert.AreEqual(0, result.Length);
        }

        /// <summary>
        /// Warn if explorer(.exe) is in 'Executables'.
        /// </summary>
        [TestMethod]
        public void WarnAboutExplorerInExecutablesTest()
        {
            // Arrange
            var scope = this.StartTestScope(typeof(ConfigurationValidationService));
            var target = scope.Get<ConfigurationValidationService>();

            var configurationModel = new ConfigurationModel
            {
                Executables = new[]
                {
                    "explorer.exe",
                },
            };

            // Act
            var result = target.Validate(configurationModel).ToArray();

            // Assert
            Assert.IsTrue(result.Length > 0);
        }
    }
}