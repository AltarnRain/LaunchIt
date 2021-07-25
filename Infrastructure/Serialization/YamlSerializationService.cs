// <copyright file="YamlSerializationService.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Serialization
{
    using Logic.Serialization;
    using YamlDotNet.Serialization;
    using YamlDotNet.Serialization.NamingConventions;

    /// <summary>
    /// Service that provides serialization and deserialization for YAML format.
    /// </summary>
    /// <seealso cref="Infrastructure.Serialization.ISerializationService" />
    public class YamlSerializationService : ISerializationService
    {
        /// <summary>
        /// The serializer.
        /// </summary>
        private ISerializer? serializer;

        /// <summary>
        /// The deserializer.
        /// </summary>
        private IDeserializer? deserializer;

        /// <summary>
        /// Serializes the specified model.
        /// </summary>
        /// <typeparam name="T">Any class.</typeparam>
        /// <param name="model">The model.</param>
        /// <returns>Serialized content.</returns>
        public string Serialize<T>(T model)
            where T : class
        {
            if (this.serializer is null)
            {
                this.serializer = new SerializerBuilder()
                    .WithTypeInspector(inner => new CommentGatheringTypeInspector(inner))
                    .WithEmissionPhaseObjectGraphVisitor(args => new CommentsObjectGraphVisitor(args.InnerVisitor))
                    .WithNamingConvention(PascalCaseNamingConvention.Instance)
                    .Build();
            }

            return this.serializer.Serialize(model);
        }

        /// <summary>
        /// Deserializes the specified text.
        /// </summary>
        /// <typeparam name="T">Any class.</typeparam>
        /// <param name="input">The text.</param>
        /// <returns>An object of type T.</returns>
        public T Deserialize<T>(string input)
            where T : class
        {
            if (this.deserializer is null)
            {
                this.deserializer = new DeserializerBuilder()
                    .WithNamingConvention(PascalCaseNamingConvention.Instance)
                    .Build();
            }

            var model = this.deserializer.Deserialize<T>(input);

            return model;
        }
    }
}
