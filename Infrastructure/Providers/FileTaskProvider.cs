// <copyright file="FileTaskProvider.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Providers
{
    using Domain.Models.Task;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Provides tasks configured in the Tasks.txt file.
    /// </summary>
    /// <seealso cref="Logic.Providers.ITaskProvider" />
    public class FileTaskProvider : Logic.Providers.ITaskProvider
    {
        private readonly Logic.Providers.IPathProvider pathProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileTaskProvider" /> class.
        /// </summary>
        /// <param name="pathProvider">The path provider.</param>
        public FileTaskProvider(Logic.Providers.IPathProvider pathProvider)
        {
            this.pathProvider = pathProvider;
        }

        /// <summary>
        /// Gets the tasks.
        /// </summary>
        /// <returns>
        /// Task model's.
        /// </returns>
        public TaskModel[] GetTasks2()
        {
            var taskFile = this.pathProvider.MapPath("~/Tasks.txt");

            if (!File.Exists(taskFile))
            {
                throw new FileNotFoundException($"Could not find file {taskFile}");
            }

            var taskFileContent = File.ReadAllLines(taskFile);

            var returnValue = new List<TaskModel>();

            foreach(var line in taskFileContent)
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

                TaskTarget? taskTarget = null;

                if (line == Domain.Constants.Sections.Services)
                {
                    taskTarget = TaskTarget.Services;
                }

                if (line == Domain.Constants.Sections.Executables)
                {
                    taskTarget = TaskTarget.Executable;
                }

                if (taskTarget is null)
                {
                    continue;
                }

                var taskModel = new TaskModel
                {
                    Name = line,
                    TaskAction = TaskAction.Stop,
                    TaskTarget = taskTarget.Value,
                };

                returnValue.Add(taskModel);
            }

            return returnValue.ToArray();
        }
    }
}
