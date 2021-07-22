// <copyright file="GameOptimizedActionHandlerTests.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Common.Tests
{
    using Domain.Models.Task;
    using global::Tests.Base;
    using global::Tests.TestImplementations;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests <see cref="GameOptimizedActionHandler"/>.
    /// </summary>
    /// <seealso cref="Tests.Base.TestBase" />
    [TestClass]
    public class GameOptimizedActionHandlerTests : TestBase
    {
        /// <summary>
        /// Handles the test.
        /// </summary>
        [TestMethod]
        public void HandleTest()
        {
            // Arrange
            var scope = this.GetTestContext();

            var target = scope.GameOptimizedActionHandler;
            var testWindowsService = (TestWindowsService)scope.WindowsServices;

            var action = new GameOptimizerActionModel
            {
                Name = "A Name",
                TaskAction = TaskAction.Stop,
                TaskTarget = TaskTarget.Services,
            };

            // Act
            target.Handle(action);

            // Assert
            Assert.IsTrue(testWindowsService.ServiceStopCalled);
        }
    }
}