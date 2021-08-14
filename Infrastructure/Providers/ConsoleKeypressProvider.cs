// <copyright file="ConsoleKeypressProvider.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Providers
{
    using Logic.Contracts.Services;
    using System;

    /// <summary>
    /// Provides keypresses from the console.
    /// </summary>
    /// <seealso cref="Infrastructure.Providers.IKeypressProvider" />
    public class ConsoleKeypressProvider : IKeypressProvider
    {
        private readonly ILogEventService logEventService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleKeypressProvider"/> class.
        /// </summary>
        /// <param name="logEventService">The log event service.</param>
        public ConsoleKeypressProvider(ILogEventService logEventService)
        {
            this.logEventService = logEventService;
        }

        /// <summary>
        /// Keypresses this instance.
        /// </summary>
        /// <typeparam name="T">Any type.</typeparam>
        /// <returns>
        /// The pressed key.
        /// </returns>
        public T Keypress<T>()
        {
            while (true)
            {
                var keyPress = System.Console.ReadKey();
                try
                {
                    var input = (T)Convert.ChangeType(keyPress.Key, typeof(T));
                    return input;
                }
                catch
                {
                    this.logEventService.Log("Invalid input.");
                }
            }
        }
    }
}
