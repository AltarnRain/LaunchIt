// <copyright file="LaunchModelProviderTests.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Providers.Tests
{
    using Domain.Models.Configuration;
    using global::Tests.Base;
    using Logic.Contracts.Services;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests the <see cref="LaunchModelProvider"/>.
    /// </summary>
    [TestClass]
    public class LaunchModelProviderTests : TestBase
    {
        /// <summary>
        /// Gets the model test.
        /// </summary>
        [TestMethod]
        public void GetModelTest()
        {
            // Arrange
            using var scope = this.StartTestScope(typeof(LaunchModelProvider));
            var target = scope.Get<LaunchModelProvider>();
            var configurationService = scope.Get<IConfigurationService>();

            var configurationModel = new ConfigurationModel
            {
                Executables = new[]
                {
                    "A",
                    "B",
                },
                Services = new[]
                {
                    "1",
                    "2",
                },
                ExecutableShutdownConfiguration = new ShutdownConfigurationModel(),
                ServiceShutdownConfiguration = new ShutdownConfigurationModel(),
                MonitoringConfiguration = new MonitoringConfiguration(),
                UseBatchFile = false,
                ShutdownExplorer = false,
            };

            configurationService.Write(configurationModel);

            var args = new[]
            {
                "-usebatch",
            };

            // Act
            var result = target.GetModel(args);

            // Assert
            Assert.AreEqual("A", result.Executables[0]);
            Assert.AreEqual("B", result.Executables[1]);
            Assert.AreEqual("1", result.Services[0]);
            Assert.AreEqual("2", result.Services[1]);
            Assert.IsTrue(result.ExecutableShutdownConfiguration == configurationModel.ExecutableShutdownConfiguration);
            Assert.IsTrue(result.ServiceShutdownConfiguration == configurationModel.ServiceShutdownConfiguration);
            Assert.IsTrue(result.MonitoringConfiguration == configurationModel.MonitoringConfiguration);
            Assert.IsTrue(result.UseBatchFile);
            Assert.IsFalse(result.ShutdownExplorer);
        }
    }
}