// <copyright file="GameOptimizedActionHandler.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Common
{
    using Domain.Models.Task;
    using Logic.Common;

    /// <summary>
    /// Handles a GameOptimizer Action model.
    /// </summary>
    /// <seealso cref="Logic.Common.IGameOptimizedActionHandler" />
    public class GameOptimizedActionHandler : IGameOptimizedActionHandler
    {
        private readonly IRunningProgramsHelper windowServices;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameOptimizedActionHandler"/> class.
        /// </summary>
        /// <param name="windowServices">The window services.</param>
        public GameOptimizedActionHandler(IRunningProgramsHelper windowServices)
        {
            this.windowServices = windowServices;
        }

        /// <summary>
        /// Resolves the specified action.
        /// </summary>
        /// <param name="action">The action.</param>
        public void Handle(GameOptimizerActionModel action)
        {
            if (action.TaskTarget == TaskTarget.Services)
            {
                if (action.TaskAction == TaskAction.Stop)
                {
                    this.windowServices.StopService(action.Name);
                    return;
                }

                if (action.TaskAction == TaskAction.Start)
                {
                    this.windowServices.StartService(action.Name);
                    return;
                }

                return;
            }

            if (action.TaskTarget == TaskTarget.Executable)
            {
                if (action.TaskAction == TaskAction.Start)
                {
                    this.windowServices.StartExecutable(action.Name);
                    return;
                }

                if (action.TaskAction == TaskAction.Stop)
                {
                    this.windowServices.StopExecutable(action.Name);
                }
            }
        }
    }
}
