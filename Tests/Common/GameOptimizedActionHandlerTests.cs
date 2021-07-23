// <copyright file="GameOptimizedActionHandlerTests.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Tests.Common
{
    using Domain.Models.Action;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Tests.Base;

    /// <summary>
    /// Tests <see cref="GameOptimizerActionHandler"/>.
    /// </summary>
    /// <seealso cref="TestBase" />
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
                    TaskTarget = ActionTarget.Services,
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
                    TaskTarget = ActionTarget.Executable,
                };

                // Act
                target.Handle(action);

                // Assert
                Assert.IsTrue(testRunningProgramsHelper.StopExecutableCalled);
            }
        }
    }
}