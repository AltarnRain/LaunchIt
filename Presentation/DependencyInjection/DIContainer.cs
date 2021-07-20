﻿// <copyright file="DIContainer.cs" company="Onno Invernizzi">
// Copyright (c) Onno Invernizzi. All rights reserved.
// </copyright>

namespace GameLauncher.DependencyInjection
{
    using StrongInject;

    /// <summary>
    /// Dependency injection container.
    /// </summary>
    /// <seealso cref="StrongInject.IContainer{Logic.Startup}" />
    [Register(typeof(Logic.Startup), Scope.SingleInstance)]
    [Register(typeof(Infrastructure.Common.WindowsService), Scope.SingleInstance, typeof(Logic.Common.IWindowServices))]
    [Register(typeof(Infrastructure.Common.ConsoleLogService), Scope.SingleInstance, typeof(Logic.Common.ILogger))]
    [Register(typeof(Infrastructure.Providers.UnstoppableServicesProvider), Scope.SingleInstance, typeof(Logic.Providers.IUnstoppableServiceProvider))]
    [Register(typeof(Infrastructure.Providers.PathProvider), Scope.SingleInstance, typeof(Logic.Providers.IPathProvider))]

    public partial class DIContainer : IContainer<Logic.Startup>
    {
        // Code is generated. Do not write implementation here.
    }
}
