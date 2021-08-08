// <copyright file="LaunchItStartupServiceTests.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Services.Tests
{
    using Domain.Models.Configuration;
    using global::Tests.Base;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests the LaunchItStartupService.
    /// </summary>
    [TestClass]
    public class LaunchItStartupServiceTests : TestBase
    {
        /// <summary>
        /// Starts the test.
        /// </summary>
        [TestMethod]
        public void StartOnlyShutdownExplorerTest()
        {
            // Arrange
            using var scope = this.StartTestScope(typeof(LaunchItStartupService));
            var target = scope.Get<LaunchItStartupService>();

            var model = new LaunchModel
            {
                ShutdownExplorer = true,
            };

            // Act
            target.Start(model);

            // Assert
            var testProcessHelper = scope.GetTestProcessHelper();
            Assert.AreEqual(1, testProcessHelper.StopCalls.Count);
            Assert.AreEqual("explorer.exe", testProcessHelper.StopCalls[0]);
        }

        /// <summary>
        /// Starts the test.
        /// </summary>
        [TestMethod]
        public void StartWithServicesAndExecutablesTest()
        {
            // Arrange
            using var scope = this.StartTestScope(typeof(LaunchItStartupService));
            var target = scope.Get<LaunchItStartupService>();

            var model = new LaunchModel
            {
                ShutdownExplorer = false,
                Services = new[]
                {
                    "A",
                    "B",
                    "C",
                },
                Executables = new[]
                {
                    "1",
                    "2",
                },
            };

            // Act
            target.Start(model);

            // Assert
            var testServiceHelper = scope.GetTestServiceHelper();
            Assert.AreEqual(3, testServiceHelper.StopCalls.Count);

            var testProcessHelper = scope.GetTestProcessHelper();
            Assert.AreEqual(2, testProcessHelper.StopCalls.Count);
        }
    }
}