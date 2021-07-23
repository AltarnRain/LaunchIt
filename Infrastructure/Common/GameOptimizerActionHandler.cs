// <copyright file="GameOptimizerActionHandler.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Common
{
    using Domain.Models.Task;
    using Logic.Common;

    /// <summary>
    /// Handles a GameOptimizer Action model.
    /// </summary>
    /// <seealso cref="Logic.Common.IGameOptimizerActionHandler" />
    public class GameOptimizerActionHandler : IGameOptimizerActionHandler
    {
        private readonly IRunningProgramsHelper runningProgramsHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameOptimizerActionHandler"/> class.
        /// </summary>
        /// <param name="windowServices">The window services.</param>
        public GameOptimizerActionHandler(IRunningProgramsHelper windowServices)
        {
            this.runningProgramsHelper = windowServices;
        }

        /// <summary>
        /// Resolves the specified action.
        /// </summary>
        /// <param name="action">The action.</param>
        public void Handle(GameOptimizerActionModel action)
        {
            if (action.TaskTarget == TaskTarget.Services)
            {
                this.runningProgramsHelper.StopService(action.Name);

                return;
            }

            if (action.TaskTarget == TaskTarget.Executable)
            {
                this.runningProgramsHelper.StopExecutable(action.Name);

                return;
            }
        }
    }
}
