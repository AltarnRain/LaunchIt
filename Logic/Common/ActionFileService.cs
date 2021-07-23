// <copyright file="ActionFileService.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Logic.Common
{
    using Domain;
    using Domain.Models.Programs;
    using Logic.Extensions;
    using Logic.Providers;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Service for the action file.
    /// </summary>
    /// <seealso cref="Logic.Common.IActionFileService" />
    public class ActionFileService : IActionFileService
    {
        private readonly IPathProvider pathProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionFileService"/> class.
        /// </summary>
        /// <param name="pathProvider">The path provider.</param>
        public ActionFileService(IPathProvider pathProvider)
        {
            this.pathProvider = pathProvider;
        }

        /// <summary>
        /// Writes the specified program models.
        /// </summary>
        /// <param name="programModels">The program models.</param>
        public void Write(IEnumerable<ProgramModel> programModels)
        {
            var actionFile = this.pathProvider.GetActionFilePath();
            this.Write(programModels, actionFile);
        }

        /// <summary>
        /// Writes the specified program models.
        /// </summary>
        /// <param name="programModels">The program models.</param>
        /// <param name="file">The file.</param>
        public void Write(IEnumerable<ProgramModel> programModels, string file)
        {
            if (File.Exists(file))
            {
                File.Delete(file);
            }

            using (var fs = new ActionFileWriter(file))
            {
                WriteHeader(fs);

                fs.WriteServiceSection();

                // Order on programs and handle services first.
                foreach (var program in programModels.Where(x => x.ProgramType == ProgramType.Service && x.Running == true))
                {
                    if (program.ServiceInfo is null)
                    {
                        continue;
                    }

                    fs.WriteLine(program.ServiceInfo.DisplayName);
                }

                fs.WriteLine(string.Empty);

                foreach (var program in programModels.Where(x => x.ProgramType == ProgramType.Executable && x.Running == true))
                {
                    fs.WriteLine(program.Name);
                }

                fs.Flush();
                fs.Close();
            }
        }

        private static void WriteHeader(ActionFileWriter fs)
        {
            fs.WriteComment("Welcome to the action file. This file is used to instruct Game Launcher which services and executables you want to shut down.");
            fs.WriteComment("The file is split up into two sections. [Services] and [Executables].");
            fs.WriteComment(string.Empty);
        }
    }
}
