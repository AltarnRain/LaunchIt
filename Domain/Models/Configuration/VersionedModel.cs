// <copyright file="VersionedModel.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Domain.Models.Configuration
{
    /// <summary>
    /// A model that has versions.
    /// </summary>
    public abstract class VersionedModel
    {
        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        public virtual int Version { get; set; }
    }
}