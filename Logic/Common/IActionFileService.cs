// <copyright file="IActionFileService.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Logic.Common
{
    using Domain.Models.Programs;
    using System.Collections.Generic;

    /// <summary>
    /// Contract for a class that can write running program models in to a Action file.
    /// </summary>
    public interface IActionFileService
    {
        /// <summary>
        /// Writes the specified program models.
        /// </summary>
        /// <param name="programModels">The program models.</param>
        public void Write(IEnumerable<ProgramModel> programModels);

        /// <summary>
        /// Writes the specified program models.
        /// </summary>
        /// <param name="programModels">The program models.</param>
        /// <param name="file">The file.</param>
        public void Write(IEnumerable<ProgramModel> programModels, string file);
    }
}
