// <copyright file="ExtensionClassTests.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Tests.Infrastructure
{
    using global::Infrastructure;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Test class for <see cref="ExtensionClass"/>.
    /// </summary>
    [TestClass]
    public class ExtensionClassTests
    {
        /// <summary>
        /// Gets the maximum array content test.
        /// </summary>
        [TestMethod]
        public void GetMaxArrayContentTest()
        {
            static void RunTest()
            {
                // Arrange
                var items = new List<object[]>
                {
                    new[] { "A", "BB", "C" },
                    new[] { "DDD" },
                    new[] { string.Empty, string.Empty, string.Empty, "EEEE" },
                    new[] { string.Empty, string.Empty, string.Empty, string.Empty },
                };

                // Act
                var result = items.GetMaximumColumnSizes();

                // Assert
                Assert.AreEqual(4, result.Count);
                Assert.AreEqual(3, result[0]);
                Assert.AreEqual(2, result[1]);
                Assert.AreEqual(4, result[3]);
            }

            RunTest();
        }

        /// <summary>
        /// Formats the table test.
        /// </summary>
        [TestMethod]
        public void FormatTableTest()
        {
            // Arrange
            static void RunTest()
            {
                var items = new List<object[]>
                {
                    new[] { "A", "BB", string.Empty, "C" },
                    new[] { "DDD" },
                    new[] { string.Empty, string.Empty, string.Empty, "EEEE" },
                    new[] { string.Empty, string.Empty, string.Empty, string.Empty },
                };

                // Act
                var formattedTable = items.FormatTable();

                // Assert
                var i = 0;
                var row1 = formattedTable.ElementAt(i++);

                var paddedA = "A".PadRight(3);
                var paddedC = "C".PadRight(4);

                var expectedRowValue = $"{paddedA} BB  {paddedC}";

                Assert.AreEqual(row1, expectedRowValue);
            }

            RunTest();
        }

        /// <summary>
        /// Formats the table with headers test.
        /// </summary>
        [TestMethod]
        public void FormatTableWithHeadersTest()
        {
            // Arrange
            static void RunTest()
            {
                var items = new List<object[]>
                {
                    new[] { "A", "BB", "CCC", "DDDD" },
                };

                // Act
                string[] headers = new[] { "Header 1", "Header 2", "Header 3", "Header 4" };
                var formattedTable = items.FormatTable(headers);

                // Assert
                var i = 0;
                var headerRow = formattedTable.ElementAt(i++);
                var row1 = formattedTable.ElementAt(i++);

                var expectedHeaderRow = string.Join(" ", headers);

                Assert.AreEqual(headerRow, expectedHeaderRow);
                Assert.AreEqual(row1, "A        BB       CCC      DDDD    ");
            }

            RunTest();
        }

        /// <summary>
        /// Formats the table with headers and seperation character test.
        /// </summary>
        [TestMethod]
        public void FormatTableWithHeadersAndSeperationCharacterTest()
        {
            // Arrange
            static void RunTest()
            {
                var items = new List<object[]>
                {
                    new[] { "A", "BB", "CCC", "DDDD" },
                };

                string[] headers = new[] { "Header 1", "Header 2", "Header 3", "Header 4" };

                // Act
                var formattedTable = items.FormatTable(headers, '|');

                // Assert
                var i = 0;
                var headerRow = formattedTable.ElementAt(i++);
                var row1 = formattedTable.ElementAt(i++);

                var expectedHeaderRow = string.Join("|", headers);

                Assert.AreEqual(headerRow, expectedHeaderRow);
                Assert.AreEqual(row1, "A|BB|CCC|DDDD");
            }

            RunTest();
        }
    }
}