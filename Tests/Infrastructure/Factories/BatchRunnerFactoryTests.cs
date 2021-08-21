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
            var result1 = target.Create("Some content");
            var result2 = target.Create("Some content");

            // Assert
            Assert.IsNotNull(result1);
            Assert.IsTrue(result1 is BatchRunner);

            // Factory, should always create a new object.
            Assert.IsFalse(result1 == result2);
        }
    }
}