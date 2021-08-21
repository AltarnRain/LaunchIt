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
            var batch1 = target.Create(batchLaunchModel);
            var batch2 = target.Create(batchLaunchModel);
            var launch1 = target.Create(launchItLaunchModel);
            var launch2 = target.Create(launchItLaunchModel);

            // Assert
            Assert.IsTrue(batch1 is BatchFileStartupService);
            Assert.IsTrue(batch2 is BatchFileStartupService);
            Assert.IsTrue(launch1 is LaunchItStartupService);
            Assert.IsTrue(launch2 is LaunchItStartupService);

            // Factory should always return a new object.
            Assert.IsFalse(launch1 == launch2);
            Assert.IsFalse(batch1 == batch2);
        }
    }
}