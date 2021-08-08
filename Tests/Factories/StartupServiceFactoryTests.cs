// <copyright file="StartupServiceFactoryTests.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Factories.Tests
{
    using Domain.Models.Configuration;
    using global::Tests.Base;
    using Infrastructure.Services;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests <see cref="StartupServiceFactory"/>.
    /// </summary>
    [TestClass]
    public class StartupServiceFactoryTests : TestBase
    {
        /// <summary>
        /// Tests <see cref="StartupServiceFactory.Create(LaunchModel)"/>.
        /// </summary>
        [TestMethod]
        public void CreateTest()
        {
            // Arrange
            using var scope = this.StartTestScope(typeof(StartupServiceFactory));
            var target = scope.Get<StartupServiceFactory>();

            var batchLaunchModel = new LaunchModel { UseBatchFile = true };
            var launchItLaunchModel = new LaunchModel { UseBatchFile = false };

            // Act
            var result1 = target.Create(batchLaunchModel);
            var result2 = target.Create(launchItLaunchModel);

            // Assert
            Assert.IsTrue(result1 is BatchFileStartupService);
            Assert.IsTrue(result2 is LaunchItStartupService);
        }
    }
}