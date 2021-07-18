using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Infrastructure.Tests
{
    [TestClass()]
    public class ExtensionClassTests
    {
        [TestMethod()]
        public void GetMaxArrayContentTest()
        {
            // Arrange
            var items = new List<string[]>
            {
                new [] {"A", "BB", "C" },
                new [] {"DDD" },
                new [] {"","","",""},
                new [] {"","","",""},
            };

            // Act
            var result = items.GetMaxArrayContent();

            // Assert
            Assert.AreEqual(4, result.Count);
            Assert.AreEqual(3, result[0]);
            Assert.AreEqual(2, result[1]);
            Assert.AreEqual(1, result[3]);
        }
    }
}