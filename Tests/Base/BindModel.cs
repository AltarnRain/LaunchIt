// <copyright file="BindModel.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Tests.Base
{
    using System;

    /// <summary>
    /// Model for binding services to implementations for testing.
    /// </summary>
    public class BindModel
    {
        /// <summary>
        /// Gets or sets the type of the service.
        /// </summary>
        /// <value>
        /// The type of the service.
        /// </value>
        public Type? ServiceType { get; set; }

        /// <summary>
        /// Gets or sets to.
        /// </summary>
        public Type? ImplementationType { get; set; }
    }
}