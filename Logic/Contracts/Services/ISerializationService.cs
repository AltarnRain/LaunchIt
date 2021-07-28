// <copyright file="ISerializationService.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Logic.Contracts.Services
{
    /// <summary>
    /// Contract for a class the provides serialization and deserialization services.
    /// </summary>
    public interface ISerializationService
    {
        /// <summary>
        /// Deserializes the specified text.
        /// </summary>
        /// <typeparam name="T">Any class.</typeparam>
        /// <param name="text">The text.</param>
        /// <returns>An object.</returns>
        T Deserialize<T>(string text)
            where T : class;

        /// <summary>
        /// Serializes the specified model.
        /// </summary>
        /// <typeparam name="T">Any class.</typeparam>
        /// <param name="model">The model.</param>
        /// <returns>Serialized content.</returns>
        string Serialize<T>(T model)
            where T : class;
    }
}