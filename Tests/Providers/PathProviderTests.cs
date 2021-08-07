// <copyright file="PathProviderTests.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Providers.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests <see cref="PathProvider"/>.
    /// </summary>
    [TestClass]
    public class PathProviderTests
    {
        /// <summary>
        /// Maps the path test.
        /// </summary>
        [TestMethod]
        public void MapPathTest()
        {
            // Arrange
            var pathProvider = new PathProvider("X:\\NotARealFolder");

            // Act
            var result1 = pathProvider.MapPath("~/SubFolder");
            var result2 = pathProvider.MapPath("~/SubFolder/AFile.txt");
            var result3 = pathProvider.MapPath("~/");

            // Assert
            Assert.AreEqual("X:\\NotARealFolder\\SubFolder", result1);
            Assert.AreEqual("X:\\NotARealFolder\\SubFolder/AFile.txt", result2);
            Assert.AreEqual("X:\\NotARealFolder", result3);
        }

        /// <summary>
        /// Throws when not prefixed with "~/".
        /// </summary>
        [TestMethod]
        public void MapNonRelativePath()
        {
            var pathProvider = new PathProvider("X:\\NotARealFolder");

            // Act
            Assert.ThrowsException<Domain.Exceptions.InvalidMapPathException>(() => pathProvider.MapPath("SubFolder"));
        }
    }
}