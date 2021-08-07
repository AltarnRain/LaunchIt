// <copyright file="FileLoggerTests.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Loggers.Tests
{
    using Infrastructure.Loggers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Linq;

    /// <summary>
    /// Tests the <see cref="FileLogger"/>.
    /// </summary>
    [TestClass]
    public class FileLoggerTests
    {
        /// <summary>
        /// Logs the test.
        /// </summary>
        [TestMethod]
        public void LogAndCloseTest()
        {
            // Arrange
            var fileLogger = new FileLogger();
            var logFile = fileLogger.FileName;

            // Act
            fileLogger.Log("Line 1");
            fileLogger.Log("Line 2");

            // Assert

            // FileLogger caches 10 lines before writing to a file. File should not exist yet.
            Assert.IsFalse(System.IO.File.Exists(logFile));

            fileLogger.Close();

            var linesAfterClose = System.IO.File.ReadAllLines(logFile);
            Assert.AreEqual(2, linesAfterClose.Length);
            Assert.AreEqual("Line 1", linesAfterClose[0]);
            Assert.AreEqual("Line 2", linesAfterClose[1]);
        }

        /// <summary>
        /// Closes the test.
        /// </summary>
        [TestMethod]
        public void TestCache()
        {
            // Arrange
            var fileLogger = new FileLogger();
            var logFile = fileLogger.FileName;
            var lines = Enumerable.Range(0, 10).Select(n => $"Line {n}").ToArray();

            // Act
            foreach (var line in lines)
            {
                fileLogger.Log(line);
            }

            fileLogger.Log("Extra line");

            // Assert

            // FileLogger caches 10 lines before writing to a file. File should exist yet.
            Assert.IsTrue(System.IO.File.Exists(logFile));
            var linesBeforeClose = System.IO.File.ReadAllLines(logFile);
            Assert.AreEqual(10, linesBeforeClose.Length);

            for (int i = 0; i < lines.Length; i++)
            {
                Assert.AreEqual(lines[i], linesBeforeClose[i]);
            }

            fileLogger.Close();

            var linesAfterClose = System.IO.File.ReadAllLines(logFile);
            Assert.AreEqual(11, linesAfterClose.Length);
            Assert.AreEqual("Extra line", linesAfterClose[10]);
        }

        /// <summary>
        /// Tests the log cache size other then ten.
        /// </summary>
        [TestMethod]
        public void TestLogCacheSizeOtherThenTen()
        {
            // Arrange
            var fileLogger = new FileLogger(0); // No cache.
            var logFile = fileLogger.FileName;

            // Act
            fileLogger.Log("Line 1");

            // Assert
            Assert.IsTrue(System.IO.File.Exists(logFile));
        }
    }
}