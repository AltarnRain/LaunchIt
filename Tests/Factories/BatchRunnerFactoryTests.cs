// <copyright file="BatchRunnerFactoryTests.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Factories.Tests
{
    using global::Tests.Base;
    using Infrastructure.Helpers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests the <see cref="BatchRunnerFactory"/>.
    /// </summary>
    [TestClass]
    public class BatchRunnerFactoryTests : TestBase
    {
        /// <summary>
        /// Creates the test.
        /// </summary>
        [TestMethod]
        public void CreateTest()
        {
            // Arrange.
            var scope = this.StartTestScope(typeof(BatchRunnerFactory));
            var target = scope.Get<BatchRunnerFactory>();

            // Act
            var result = target.Create("Some content");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result is BatchRunner);
        }
    }
}