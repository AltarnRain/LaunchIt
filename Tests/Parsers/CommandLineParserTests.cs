// <copyright file="CommandLineParserTests.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Parsers.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests the <see cref="CommandLineParser.Parse(string)"/> method.
    /// </summary>
    [TestClass]
    public class CommandLineParserTests
    {
        /// <summary>
        /// Parses the single switch with options.
        /// </summary>
        [TestMethod]
        public void ParseSingleSwitchWithOptions()
        {
            // Act
            var result = CommandLineParser.Parse("-priority=High, Low, Real Time");

            // Assert
            Assert.AreEqual("priority", result[0].Command);
            Assert.AreEqual(3, result[0].Options.Length);

            var options = result[0].Options;

            Assert.AreEqual("High", options[0]);
            Assert.AreEqual("Low", options[1]);
            Assert.AreEqual("Real Time", options[2]);
        }

        /// <summary>
        /// Parses the multiple switches with options end with optionless switch.
        /// </summary>
        [TestMethod]
        public void ParseMultipleSwitchesWithOptionsEndWithOptionlessSwitch()
        {
            // Act
            var result = CommandLineParser.Parse("-priority=High, Low, Real Time -services=Service 1, Service 2, Service 3, Service 4 -usebatch");

            // Assert
            Assert.AreEqual(3, result.Length);

            var firstResult = result[0];
            Assert.AreEqual("priority", firstResult.Command);
            Assert.AreEqual(3, firstResult.Options.Length);

            var options1 = firstResult.Options;

            Assert.AreEqual("High", options1[0]);
            Assert.AreEqual("Low", options1[1]);
            Assert.AreEqual("Real Time", options1[2]);

            var secondResult = result[1];
            Assert.AreEqual(4, secondResult.Options.Length);

            var options2 = secondResult.Options;

            Assert.AreEqual("Service 1", options2[0]);
            Assert.AreEqual("Service 2", options2[1]);
            Assert.AreEqual("Service 3", options2[2]);
            Assert.AreEqual("Service 4", options2[3]);

            var thirdResult = result[2];
            Assert.AreEqual("usebatch", thirdResult.Command);
            Assert.AreEqual(0, thirdResult.Options.Length);
        }

        /// <summary>
        /// Parses the optionsless switch followed by one with options ending with one without.
        /// </summary>
        [TestMethod]
        public void ParseOptionslessSwitchFollowedByOneWithOptionsEndingWithOneWithout()
        {
            // Act
            var result = CommandLineParser.Parse("-usebatch -priority=High, Low, Real Time -abc");

            // Assert
            Assert.AreEqual(3, result.Length);

            var firstResult = result[0];
            Assert.AreEqual("usebatch", firstResult.Command);
            Assert.AreEqual(0, firstResult.Options.Length);

            var secondResult = result[1];
            Assert.AreEqual("priority", secondResult.Command);
            Assert.AreEqual(3, secondResult.Options.Length);

            var options1 = secondResult.Options;
            Assert.AreEqual("High", options1[0]);
            Assert.AreEqual("Low", options1[1]);
            Assert.AreEqual("Real Time", options1[2]);

            var thirdResult = result[2];
            Assert.AreEqual("abc", thirdResult.Command);
        }
    }
}