// <copyright file="CounterTests.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Utility.Tests
{
    using Infrastructure.Utility;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test <see cref="Counter"/>.
    /// </summary>
    [TestClass]
    public class CounterTests
    {
        /// <summary>
        /// Adds the test.
        /// </summary>
        [TestMethod]
        public void AddTest()
        {
            // Arrange
            var counter = new Counter();

            // Act
            counter.Add("Count1");
            counter.Add("Count1");
            counter.Add("Count1");

            counter.Add("Count2");
            counter.Add("Count2");

            counter.Add("Count3");

            // Assert
            Assert.AreEqual(3, counter["Count1"]);
            Assert.AreEqual(2, counter["Count2"]);
            Assert.AreEqual(1, counter["Count3"]);
        }
    }
}