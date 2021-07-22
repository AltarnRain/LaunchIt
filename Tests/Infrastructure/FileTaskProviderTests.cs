// <copyright file="FileTaskProviderTests.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Tests.Infrastructure
{
    using Domain.Models.Task;
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
                sw.WriteLine("# I am a comment.");
                sw.WriteLine(Domain.Constants.Sections.Services);
                sw.WriteLine("Service A");
                sw.WriteLine(string.Empty);
                sw.WriteLine("Service B");
                sw.WriteLine(string.Empty);
                sw.WriteLine("# I am also a comment");
                sw.WriteLine(Domain.Constants.Sections.Executables);
                sw.WriteLine("Executable A");
                sw.WriteLine("Executable B");
                sw.Flush();
                sw.Close();
            }

            // Act
            var result = target.GetGameOptimizerActions();

            // Assert
            Assert.AreEqual(4, result.Length);

            var i = 0;
            var task1 = result[i++];
            var task2 = result[i++];
            var task3 = result[i++];
            var task4 = result[i++];

            Assert.AreEqual("Service A", task1.Name);
            Assert.AreEqual(TaskAction.Stop, task1.TaskAction);
            Assert.AreEqual(TaskTarget.Services, task1.TaskTarget);

            Assert.AreEqual("Service B", task2.Name);
            Assert.AreEqual(TaskAction.Stop, task2.TaskAction);
            Assert.AreEqual(TaskTarget.Services, task2.TaskTarget);

            Assert.AreEqual("Executable A", task3.Name);
            Assert.AreEqual(TaskAction.Stop, task3.TaskAction);
            Assert.AreEqual(TaskTarget.Executable, task3.TaskTarget);

            Assert.AreEqual("Executable B", task4.Name);
            Assert.AreEqual(TaskAction.Stop, task4.TaskAction);
            Assert.AreEqual(TaskTarget.Executable, task4.TaskTarget);
        }
    }
}
