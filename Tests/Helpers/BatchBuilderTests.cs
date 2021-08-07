// <copyright file="BatchBuilderTests.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Tests.Helpers
{
    using Infrastructure.Helpers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    /// <summary>
    /// Tests <see cref="BatchBuilder"/>.
    /// </summary>
    [TestClass]
    public class BatchBuilderTests
    {
        /// <summary>
        /// Tests object construction.
        /// </summary>
        [TestMethod]
        public void BatchBuilderTest()
        {
            // Arrange & Act
            var target = new BatchBuilder();

            // Assert
            Assert.IsNotNull(target);

            Assert.AreEqual("@echo off", target.LineAt(0));
            Assert.AreEqual("@cls", target.LineAt(1));
        }

        /// <summary>
        /// Tests <see cref="BatchBuilder.Add(string)"/>.
        /// </summary>
        [TestMethod]
        public void AddTest()
        {
            // Arrange
            var target = new BatchBuilder();

            // Act
            target.Add("This is a line");

            // Assert
            Assert.AreEqual("This is a line", target.LineAt(2));
        }

        /// <summary>
        /// Tests <see cref="BatchBuilder.Echo(string)"/>.
        /// </summary>
        [TestMethod]
        public void EchoTest()
        {
            // Arrange
            var target = new BatchBuilder();

            // Act
            target.Echo("This is an echo");

            // Assert
            Assert.AreEqual("@echo This is an echo", target.LineAt(2));
        }

        /// <summary>
        /// Tests <see cref="BatchBuilder.Rem(string)"/>.
        /// </summary>
        [TestMethod]
        public void RemTest()
        {
            // Arrange
            var target = new BatchBuilder();

            // Act
            target.Rem("This is a rem.");

            // Assert
            Assert.AreEqual("@rem This is a rem.", target.LineAt(2));
        }

        /// <summary>
        /// Tests <see cref="BatchBuilder.Pause"/>.
        /// </summary>
        [TestMethod]
        public void PauseTest()
        {
            // Arrange
            var target = new BatchBuilder();

            // Act
            target.Pause();

            // Assert
            Assert.AreEqual("@pause", target.LineAt(2));
        }

        /// <summary>
        /// Test <see cref="BatchBuilder.AddEmptyLine"/>.
        /// </summary>
        [TestMethod]
        public void AddEmptyLineTest()
        {
            // Arrange
            var target = new BatchBuilder();

            // Act
            target.AddEmptyLine();

            // Assert
            Assert.AreEqual(string.Empty, target.LineAt(2));
        }

        /// <summary>
        /// Converts to stringtest.
        /// </summary>
        [TestMethod]
        public void ToStringTest()
        {
            // Arrange
            var target = new BatchBuilder();
            target.Echo("Hello");
            target.Echo("World");

            // Act
            var stringValue = target.ToString();

            // Assert
            Assert.AreEqual($"@echo off{Environment.NewLine}@cls{Environment.NewLine}@echo Hello{Environment.NewLine}@echo World", stringValue);
        }
    }
}