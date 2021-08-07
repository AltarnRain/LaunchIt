// <copyright file="IEditorService.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Services
{
    /// <summary>
    /// Contract for a service the allows editing of a file.
    /// </summary>
    public interface IEditorService
    {
        /// <summary>
        /// Edits the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        void Edit(string file);
    }
}