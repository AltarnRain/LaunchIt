// <copyright file="ExtensionClassTests.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Logic.Extensions.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests extension methods.
    /// </summary>
    [TestClass]
    public class ExtensionClassTests
    {
        /// <summary>
        /// Determines whether [is switch test].
        /// </summary>
        [TestMethod]
        public void IsSwitchTest()
        {
            // Act
            var result1 = "-reset".IsSwitchCommand();
            var result2 = "/reset".IsSwitchCommand();
            var result3 = "reset".IsSwitchCommand();

            // Assert
            Assert.IsTrue(result1);
            Assert.IsTrue(result2);
            Assert.IsFalse(result3);
        }

        /// <summary>
        /// Gets the command line argument test.
        /// </summary>
        [TestMethod]
        public void GetCommandLineArgumentTest()
        {
            // Act
            var result = SwitchCommands.ShutdownExplorer.GetCommandLineArgument();

            // Assert
            Assert.AreEqual("-shutdownexplorer", result);
        }

        /// <summary>
        /// Gets the switch command test.
        /// </summary>
        [TestMethod]
        public void GetSwitchCommandTest()
        {
            // Act
            var result1 = "-ShutdownExplorer".GetSwitchCommand();
            var result2 = "-IAMNotSupported".GetSwitchCommand();
            var result3 = "NotASwitch".GetSwitchCommand();

            // Assert
            Assert.AreEqual(SwitchCommands.ShutdownExplorer, result1);
            Assert.AreEqual(SwitchCommands.Unknown, result2);
            Assert.AreEqual(SwitchCommands.Unknown, result3);
        }
    }
}