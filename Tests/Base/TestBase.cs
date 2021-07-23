// <copyright file="TestBase.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Tests.Base
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Base class tests that require Dependency Injection.
    /// </summary>
    [TestClass]
    public abstract class TestBase
    {
        /// <summary>
        /// Gets or sets the test context.
        /// </summary>
        public TestContext? TestContext { get; set; }

        /// <summary>
        /// Starts the test scope.
        /// </summary>
        /// <returns>A TestScope object.</returns>
        public TestScope StartTestScope()
        {
            if (this.TestContext is null)
            {
                throw new System.Exception("TestContext is not set.");
            }

            return new TestScope(this.TestContext.TestRunDirectory);
        }
    }
}
