// <copyright file="Program.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Presentation
{
    using Logic;
    using Microsoft.Extensions.Configuration;
    using Ninject;
    using System.IO;
    using System.Reflection;

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
            // Get the root path for the application. The DIContainer needs this to construct classes that use the
            // rootpath to resolve other paths.
            var rootPath = GetRootPath();

            using var kernel = new StandardKernel(new PresentationBindings(rootPath));

            var startUp = kernel.Get<Startup>();
            startUp.Start(args);
        }

        private static string GetRootPath()
        {
            var production = IsProduction();
            if (production)
            {
                return Path.GetFileName(Assembly.GetExecutingAssembly().Location);
            }
            else
            {
                return @"C:\Reps\GameLauncher\";
            }
        }

        private static bool IsProduction()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var production = configuration["Environment:Production"] == "True";
            return production;
        }
    }
}
