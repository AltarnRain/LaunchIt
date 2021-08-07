// <copyright file="CommentGatheringTypeInspector.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using YamlDotNet.Serialization;
    using YamlDotNet.Serialization.TypeInspectors;

    /// <summary>
    /// Gathers comments
    /// Source: https://dotnetfiddle.net/8M6iIE.
    /// </summary>
    [ExcludeFromCodeCoverage(Justification = "Code copied from the internet. only for adding comments to a Yaml file. Works just fine.")]
    public partial class CommentGatheringTypeInspector : TypeInspectorSkeleton
    {
        private readonly ITypeInspector innerTypeDescriptor;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentGatheringTypeInspector"/> class.
        /// </summary>
        /// <param name="innerTypeDescriptor">The inner type descriptor.</param>
        /// <exception cref="ArgumentNullException">innerTypeDescriptor.</exception>
        public CommentGatheringTypeInspector(ITypeInspector innerTypeDescriptor)
        {
            this.innerTypeDescriptor = innerTypeDescriptor ?? throw new ArgumentNullException(nameof(innerTypeDescriptor));
        }

        /// <summary>
        /// Gets the properties.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="container">The container.</param>
        /// <returns>Property descriptions.</returns>
        public override IEnumerable<IPropertyDescriptor> GetProperties(Type type, object? container)
        {
            return this.innerTypeDescriptor
                    .GetProperties(type, container)
                    .Select(d => new CommentsPropertyDescriptor(d));
        }
    }
}