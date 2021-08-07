// <copyright file="Program.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Presentation
{
    using Microsoft.Extensions.DependencyInjection;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Runtime.Versioning;

    /// <summary>
    /// Program entry point.
    /// </summary>
    [SupportedOSPlatform("windows")]
    [ExcludeFromCodeCoverage(Justification = "Program entry point.")]
    public class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            // Get the root path for the application. The DIContainer needs this to construct classes that use the
            // rootpath to resolve other paths.
            var rootPath = GetRootPath();

            var builder = LaunchItHostBuilder.Create(rootPath).Build();
            var startup = builder.Services.GetRequiredService<Startup>();

            startup.Run(args);
        }

        /// <summary>
        /// Gets the root path.
        /// </summary>
        /// <returns>The root path for the application.</returns>
        private static string GetRootPath()
        {
            return Path.GetDirectoryName(System.AppContext.BaseDirectory) ?? string.Empty;
        }
    }
}
