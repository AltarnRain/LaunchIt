// <copyright file="RetryTests.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Helpers.Tests
{
    using global::Infrastructure.Helpers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests <see cref="Retry"/>.
    /// </summary>
    [TestClass]
    public class RetryTests
    {
        /// <summary>
        /// Tets if fail is called if the 'action' returns false.
        /// </summary>
        [TestMethod]
        public void TryFailTest()
        {
            Retry.Try(
                () => false,
                (c) => Assert.AreEqual(3, c),
                (c) => Assert.Fail("Code should not be called."));
        }

        /// <summary>
        /// Tets if succes is called if the 'action' returns true.
        /// </summary>
        [TestMethod]
        public void TrySuccesTest()
        {
            Retry.Try(
                () => true,
                (c) => Assert.Fail("Code should not be called."),
                (c) => Assert.AreEqual(1, c));
        }

        /// <summary>
        /// Tets if succes is called if the 'action' returns true the second time it is called.
        /// </summary>
        [TestMethod]
        public void TryCountTest()
        {
            var i = 0;
            Retry.Try(
                () =>
                {
                    var returnValue = i != 0;
                    i++;
                    return returnValue;
                },
                (c) => Assert.Fail("Code should not be called"),
                (c) => Assert.AreEqual(2, c));
        }
    }
}