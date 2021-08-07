// <copyright file="CommentsObjectDescriptor.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Serialization
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using YamlDotNet.Core;
    using YamlDotNet.Serialization;

    /// <summary>
    /// The Comment Object descriptor.
    /// Source: https://dotnetfiddle.net/8M6iIE.
    /// </summary>
    /// <seealso cref="YamlDotNet.Serialization.IObjectDescriptor" />
    [ExcludeFromCodeCoverage(Justification = "Code copied from the internet. only for adding comments to a Yaml file. Works just fine.")]
    public sealed class CommentsObjectDescriptor : IObjectDescriptor
    {
        private readonly IObjectDescriptor innerDescriptor;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentsObjectDescriptor"/> class.
        /// </summary>
        /// <param name="innerDescriptor">The inner descriptor.</param>
        /// <param name="comment">The comment.</param>
        public CommentsObjectDescriptor(IObjectDescriptor innerDescriptor, string comment)
        {
            this.innerDescriptor = innerDescriptor;
            this.Comment = comment;
        }

        /// <summary>
        /// Gets the comment.
        /// </summary>
        public string Comment { get; private set; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public object? Value => this.innerDescriptor.Value;

        /// <summary>
        /// Gets the type that should be used when to interpret the <see cref="P:YamlDotNet.Serialization.IObjectDescriptor.Value" />.
        /// </summary>
        public Type Type => this.innerDescriptor.Type;

        /// <summary>
        /// Gets the type of <see cref="P:YamlDotNet.Serialization.IObjectDescriptor.Value" /> as determined by its container (e.g. a property).
        /// </summary>
        public Type StaticType => this.innerDescriptor.StaticType;

        /// <summary>
        /// Gets the style to be used for scalars.
        /// </summary>
        public ScalarStyle ScalarStyle => this.innerDescriptor.ScalarStyle;
    }
}