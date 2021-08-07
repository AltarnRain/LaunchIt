// <copyright file="ConsoleLoggerTests.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Loggers.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests <see cref="ConsoleLogger"/>.
    /// </summary>
    [TestClass]
    public class ConsoleLoggerTests
    {
        /// <summary>
        /// Logs the test.
        /// </summary>
        [TestMethod]
        public void LogTest()
        {
            // Arrange
            var target = new ConsoleLogger();

            // Act
            target.Log("Hello world");
        }
    }
}