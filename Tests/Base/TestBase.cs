// <copyright file="TestBase.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Tests.Base
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

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
        /// <param name="bindModels">The bind models.</param>
        /// <returns>
        /// A TestScope object.
        /// </returns>
        /// <exception cref="Exception">TestContext is not set.</exception>
        public TestScope StartTestScope(BindModel[]? bindModels = null)
        {
            if (this.TestContext is null)
            {
                throw new Exception("TestContext is not set.");
            }

            return new TestScope(this.TestContext.TestRunDirectory, bindModels);
        }

        /// <summary>
        /// Starts the test scope for a specific type. Use this when testing a service that is normally implemented as a test implementation.
        /// </summary>
        /// <param name="serviceType">The type.</param>
        /// <param name="implementationType">Type of the implementation.</param>
        /// <returns>
        /// A test scope.
        /// </returns>
        public TestScope StartTestScope(Type serviceType, Type? implementationType = null)
        {
            var bindModel = new BindModel
            {
                ServiceType = serviceType,
                ImplementationType = implementationType,
            };

            return this.StartTestScope(new[] { bindModel });
        }
    }
}
