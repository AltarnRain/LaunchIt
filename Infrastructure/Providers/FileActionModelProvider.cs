// <copyright file="FileActionModelProvider.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Providers
{
    using Domain.Models.Action;
    using Logic.Extensions;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Provides tasks configured in the Actions.txt file.
    /// </summary>
    /// <seealso cref="Logic.Providers.IActionModelProvider" />
    public class FileActionModelProvider : Logic.Providers.IActionModelProvider
    {
        private readonly Logic.Providers.IPathProvider pathProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileActionModelProvider" /> class.
        /// </summary>
        /// <param name="pathProvider">The path provider.</param>
        public FileActionModelProvider(Logic.Providers.IPathProvider pathProvider)
        {
            this.pathProvider = pathProvider;
        }

        /// <summary>
        /// Gets the tasks.
        /// </summary>
        /// <returns>
        /// Task model's.
        /// </returns>
        public GameOptimizerActionModel[] GetActions()
        {
            var taskFile = this.pathProvider.GetActionFilePath();

            if (!File.Exists(taskFile))
            {
                throw new FileNotFoundException($"Could not find file {taskFile}");
            }

            var taskFileContent = File.ReadAllLines(taskFile);

            var returnValue = new List<GameOptimizerActionModel>();

            ActionTarget? taskTarget = null;
            foreach (var line in taskFileContent)
            {
                // Ignore line, this is a comment.
                if (line.StartsWith("#"))
                {
                    continue;
                }

                // Nothing to do.
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                if (line == Domain.Constants.Sections.Services)
                {
                    taskTarget = ActionTarget.Services;
                    continue;
                }
                else if (line == Domain.Constants.Sections.Executables)
                {
                    taskTarget = ActionTarget.Executable;
                    continue;
                }

                if (taskTarget is null)
                {
                    continue;
                }

                var taskModel = new GameOptimizerActionModel
                {
                    Name = line,
                    TaskTarget = taskTarget.Value,
                };

                returnValue.Add(taskModel);
            }

            return returnValue.ToArray();
        }
    }
}
