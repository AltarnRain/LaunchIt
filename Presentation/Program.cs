// <copyright file="Program.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Presentation
{
    using Logic.Contracts.Services;
    using Ninject;
    using System.IO;
    using System.Reflection;
    using System.Runtime.Versioning;

    /// <summary>
    /// Program entry point.
    /// </summary>
    [SupportedOSPlatform("windows")]
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

            // Initialize an empty kernel.
            using var kernel = new StandardKernel();

            // Load the initial bindings.
            kernel.Load(new SharedBindings(rootPath));

            // We now have a configuration service we can use. Lets grab it straight from the kernel to keep things simple
            // and retrieve a ConfigurationModel.
            var configuration = kernel.Get<IConfigurationService>().Read();

            // Configure bindings that are set to different classes depending on configuration.
            var configurationDependentBindings = new ConfigurationDependentBindings(configuration);

            // Load it.
            kernel.Load(configurationDependentBindings);

            // All done. Lets launch it!
            var launch = kernel.Get<Launch>();

            var argument = args.Length >= 1 ? args[0] : string.Empty;

            launch.Run(argument);
        }

        /// <summary>
        /// Gets the root path.
        /// </summary>
        /// <returns>The root path for the application.</returns>
        private static string GetRootPath()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
        }
    }
}
