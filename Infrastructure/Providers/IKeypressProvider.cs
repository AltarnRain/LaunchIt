// <copyright file="IKeypressProvider.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Providers
{
    /// <summary>
    /// Conteact for a class the provides keypresses.
    /// </summary>
    public interface IKeypressProvider
    {
        /// <summary>
        /// Keypresses this instance.
        /// </summary>
        /// <typeparam name="T">Any type.</typeparam>
        /// <returns>The pressed key.</returns>
        T Keypress<T>();
    }
}