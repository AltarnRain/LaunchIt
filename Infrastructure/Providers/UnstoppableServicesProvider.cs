// <copyright file="UnstoppableServicesProvider.cs" company="Onno Invernizzi">
// Copyright (c) Onno Invernizzi. All rights reserved.
// </copyright>

namespace Infrastructure.Providers
{
    using Logic.Providers;
    using System;
    using System.IO;

    /// <summary>
    /// Provides names of services that cannot be stopped.
    /// </summary>
    /// <seealso cref="Logic.Providers.IUnstoppableServiceProvider" />
    public class UnstoppableServicesProvider : IUnstoppableServiceProvider
    {
        private readonly IPathProvider pathProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnstoppableServicesProvider"/> class.
        /// </summary>
        /// <param name="pathProvider">The path provider.</param>
        public UnstoppableServicesProvider(IPathProvider pathProvider)
        {
            this.pathProvider = pathProvider;
        }

        /// <summary>
        /// Gets the unstoppable services.
        /// </summary>
        /// <returns>Unstoppable servcie names.</returns>
        public string[] GetUnstoppableServices()
        {
            var ignoredServicesFile = this.pathProvider.MapPath("~/ignoredservices.dat");

            if (!File.Exists(ignoredServicesFile))
            {
                return Array.Empty<string>();
            }

            return File.ReadAllLines(ignoredServicesFile);
        }
    }
}
