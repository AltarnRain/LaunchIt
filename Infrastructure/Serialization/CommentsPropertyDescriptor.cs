// <copyright file="CommentsPropertyDescriptor.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Serialization
{
    using System;
    using System.ComponentModel;
    using YamlDotNet.Core;
    using YamlDotNet.Serialization;

    /// <summary>
    /// The comments property descriptor.
    /// Source: https://dotnetfiddle.net/8M6iIE.
    /// </summary>
    /// <seealso cref="YamlDotNet.Serialization.IPropertyDescriptor" />
    public sealed class CommentsPropertyDescriptor : IPropertyDescriptor
    {
        private readonly IPropertyDescriptor baseDescriptor;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentsPropertyDescriptor"/> class.
        /// </summary>
        /// <param name="baseDescriptor">The base descriptor.</param>
        public CommentsPropertyDescriptor(IPropertyDescriptor baseDescriptor)
        {
            this.baseDescriptor = baseDescriptor;
            this.Name = baseDescriptor.Name;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the type.
        /// </summary>
        public Type Type => this.baseDescriptor.Type;

        /// <summary>
        /// Gets or sets the type override.
        /// </summary>
        public Type? TypeOverride
        {
            get { return this.baseDescriptor.TypeOverride; }
            set { this.baseDescriptor.TypeOverride = value; }
        }

        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Gets or sets the scalar style.
        /// </summary>
        public ScalarStyle ScalarStyle
        {
            get { return this.baseDescriptor.ScalarStyle; }
            set { this.baseDescriptor.ScalarStyle = value; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance can write.
        /// </summary>
        public bool CanWrite => this.baseDescriptor.CanWrite;

        /// <summary>
        /// Writes the specified target.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="value">The value.</param>
        public void Write(object target, object? value)
        {
            this.baseDescriptor.Write(target, value);
        }

        /// <summary>
        /// Gets the custom attribute.
        /// </summary>
        /// <typeparam name="T">A Type.</typeparam>
        /// <returns>A custom attribute.</returns>
        public T GetCustomAttribute<T>()
            where T : Attribute
        {
            return this.baseDescriptor.GetCustomAttribute<T>();
        }

        /// <summary>
        /// Reads the specified target.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns>An IObjectDescriptor.</returns>
        public IObjectDescriptor Read(object target)
        {
            var description = this.baseDescriptor.GetCustomAttribute<DescriptionAttribute>();
            return description != null
                ? new CommentsObjectDescriptor(this.baseDescriptor.Read(target), description.Description)
                : this.baseDescriptor.Read(target);
        }
    }
}