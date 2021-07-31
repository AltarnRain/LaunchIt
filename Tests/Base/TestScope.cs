// <copyright file="TestScope.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Tests.Base
{
    using Infrastructure.Services;
    using Logic.Contracts.Providers;
    using Logic.Contracts.Services;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using System;

    /// <summary>
    /// Test Scope.
    /// </summary>
    public class TestScope : IDisposable
    {
        private readonly IHost host;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestScope" /> class.
        /// </summary>
        /// <param name="rootPath">The root path.</param>
        public TestScope(string rootPath)
        {
            var builder = Presentation.LaunchItHostBuilder.Create(rootPath);

            builder.ConfigureServices((service) =>
            {
                service.AddSingleton<ConfigurationService>();
            });

            this.host = builder.Build();
        }

        /// <summary>
        /// Gets the path provider.
        /// </summary>
        public IPathProvider PathProvider => this.host.Services.GetRequiredService<IPathProvider>();

        /// <summary>
        /// Gets the yaml configuration service.
        /// </summary>
        public ConfigurationService ConfigurationService => this.host.Services.GetRequiredService<ConfigurationService>();

        /// <summary>
        /// Gets the log event service.
        /// </summary>
        public ILogEventService LogEventService => this.host.Services.GetRequiredService<ILogEventService>();

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.host.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}