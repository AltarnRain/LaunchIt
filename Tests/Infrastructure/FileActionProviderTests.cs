// <copyright file="FileActionProviderTests.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Tests.Infrastructure
{
    using Domain;
    using Domain.Models.Task;
    using Logic.Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests the File Task Provider.
    /// </summary>
    [TestClass]
    public class FileActionProviderTests : Base.TestBase
    {
        /// <summary>
        /// Tests the get tasks.
        /// </summary>
        [TestMethod]
        public void TestGetTasks()
        {
            // Arrange
            using var scope = this.StartTestScope();
            var target = scope.FileActionModelProvider;
            var pathProvider = scope.PathProvider;

            var actionFile = pathProvider.GetActionFilePath();

            using (var sw = new ActionFileWriter(actionFile))
            {
                sw.WriteComment("I am a comment.");
                sw.WriteServiceSection();
                sw.WriteLine("Service A");
                sw.WriteLine(string.Empty);
                sw.WriteLine("Service B");
                sw.WriteLine(string.Empty);
                sw.WriteComment("I am also a comment");
                sw.WriteExecutableSection();
                sw.WriteLine("Executable A");
                sw.WriteLine("Executable B");
                sw.Flush();
                sw.Close();
            }

            // Act
            var result = target.GetActions();

            // Assert
            Assert.AreEqual(4, result.Length);

            var i = 0;
            var task1 = result[i++];
            var task2 = result[i++];
            var task3 = result[i++];
            var task4 = result[i++];

            Assert.AreEqual("Service A", task1.Name);
            Assert.AreEqual(TaskTarget.Services, task1.TaskTarget);

            Assert.AreEqual("Service B", task2.Name);
            Assert.AreEqual(TaskTarget.Services, task2.TaskTarget);

            Assert.AreEqual("Executable A", task3.Name);
            Assert.AreEqual(TaskTarget.Executable, task3.TaskTarget);

            Assert.AreEqual("Executable B", task4.Name);
            Assert.AreEqual(TaskTarget.Executable, task4.TaskTarget);
        }
    }
}
