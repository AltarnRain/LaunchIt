// <copyright file="Program.cs" company="Onno Invernizzi">
// Copyright (c) Onno Invernizzi. All rights reserved.
// </copyright>

namespace GameOptimizer
{
    using GameLauncher.DependencyInjection;
    using StrongInject;

    /// <summary>
    /// Program entry point.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            // Create a DIContainer and run the main application.
            using (var container = new DIContainer())
            {
                container.Run(x => x.Start(args));
            }
        }
    }
}
