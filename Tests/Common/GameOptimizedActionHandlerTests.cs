﻿// <copyright file="GameOptimizedActionHandlerTests.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Common.Tests
{
    using Domain.Models.Task;
    using global::Tests.Base;
    using global::Tests.TestImplementations;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests <see cref="GameOptimizerActionHandler"/>.
    /// </summary>
    /// <seealso cref="Tests.Base.TestBase" />
    [TestClass]
    public class GameOptimizedActionHandlerTests : TestBase
    {
        /// <summary>
        /// Handles the test.
        /// </summary>
        [TestMethod]
        public void HandleServiceTest()
        {
            // Arrange
            using (var scope = this.StartTestScope())
            {
                var testRunningProgramsHelper = scope.TestRunningProgramsHelper;
                var target = scope.GameOptimizerActionHandler;

                var action = new GameOptimizerActionModel
                {
                    Name = "A Name",
                    TaskTarget = TaskTarget.Services,
                };

                // Act
                target.Handle(action);

                // Assert
                Assert.IsTrue(testRunningProgramsHelper.ServiceStopCalled);
            }
        }

        /// <summary>
        /// Handles the test.
        /// </summary>
        [TestMethod]
        public void HandleExecutableTest()
        {
            // Arrange
            using (var scope = this.StartTestScope())
            {
                var testRunningProgramsHelper = scope.TestRunningProgramsHelper;
                var target = scope.GameOptimizerActionHandler;

                var action = new GameOptimizerActionModel
                {
                    Name = "A Name",
                    TaskTarget = TaskTarget.Executable,
                };

                // Act
                target.Handle(action);

                // Assert
                Assert.IsTrue(testRunningProgramsHelper.StopExecutableCalled);
            }
        }
    }
}