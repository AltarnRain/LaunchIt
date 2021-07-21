// <copyright file="FileTaskProviderTests.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Tests.Infrastructure
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests the File Task Provider.
    /// </summary>
    [TestClass]
    public class FileTaskProviderTests : Base.TestBase
    {
        /// <summary>
        /// Tests the get tasks.
        /// </summary>
        [TestMethod]
        public void TestGetTasks()
        {
            // Arrange
            var scope = this.GetTestContext();
            var target = scope.FileTaskProvider;
            var pathProvider = scope.PathProvider;

            var taskFile = pathProvider.MapPath("~/Tasks.txt");
            using (var sw = System.IO.File.AppendText(taskFile))
            {
                sw.WriteLine(Domain.Constants.Sections.Services);
                sw.WriteLine("Service A");
                sw.WriteLine("Service B");
                sw.WriteLine(Domain.Constants.Sections.Executables);
                sw.WriteLine("Executable A");
                sw.WriteLine("Executable B");
                sw.Flush();
                sw.Close();
            }

            // Act
            var result = target.GetTasks2();

            // Assert
            Assert.AreEqual(4, result.Length);
        }
    }
}
