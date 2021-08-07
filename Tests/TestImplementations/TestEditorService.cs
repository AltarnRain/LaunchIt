// <copyright file="TestEditorService.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Tests.TestImplementations
{
    using Infrastructure.Services;

    /// <summary>
    /// Test implementation for <see cref="IEditorService"/>.
    /// </summary>
    /// <seealso cref="Infrastructure.Services.IEditorService" />
    public class TestEditorService : IEditorService
    {
        /// <summary>
        /// Gets the edit calls.
        /// </summary>
        public int EditCalls { get; private set; }

        /// <summary>
        /// Edits the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        public void Edit(string file)
        {
            // Keep track of calls to this method so we can assert them.
            this.EditCalls++;
        }
    }
}
