// <copyright file="YamlSerializationServiceTests.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Serialization.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    /// <summary>
    /// Tests <see cref="YamlSerializationService"/>.
    /// </summary>
    [TestClass]
    public class YamlSerializationServiceTests
    {
        /// <summary>
        /// Test serialize. Fixates PascalCase for output.
        /// </summary>
        [TestMethod]
        public void SerializeTest()
        {
            // Arrange
            var target = new YamlSerializationService();
            var source = new TestModel { Name = "John Doe" };

            // Act
            var result = target.Serialize(source);

            // Assert
            Assert.AreEqual("Name: John Doe" + Environment.NewLine, result);
        }

        /// <summary>
        /// Test deserialize. Fixates PascalCase input and ingnoring 'nonsense' in the file.
        /// </summary>
        [TestMethod]
        public void DeserializeTest()
        {
            // Arrange
            var target = new YamlSerializationService();
            var source = "Name: John Doe" + Environment.NewLine + "Age: 11";

            // Act
            var result = target.Deserialize<TestModel>(source);

            // Assert
            Assert.AreEqual("John Doe", result.Name);
        }

        /// <summary>
        /// Test class for serializing.
        /// </summary>
        private class TestModel
        {
            /// <summary>
            /// Gets or sets the name.
            /// </summary>
            public string Name { get; set; } = string.Empty;
        }
    }
}