// <copyright file="EditorService.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Services
{
    using Infrastructure.Helpers;
    using Logic.Contracts.Services;

    /// <summary>
    /// Service the provides editing of a file.
    /// </summary>
    public class EditorService : IEditorService
    {
        private readonly IConfigurationService configurationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditorService"/> class.
        /// </summary>
        /// <param name="configurationService">The configuration service.</param>
        public EditorService(IConfigurationService configurationService)
        {
            this.configurationService = configurationService;
        }

        /// <summary>
        /// Edits the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        public void Edit(string file)
        {
            ProcessWrapper.Start(this.GetEditor(), file)?.WaitForExit();
        }

        private string GetEditor()
        {
            var returnValue = "notepad.exe";

            try
            {
                var configuration = this.configurationService.Read();
                return configuration.PreferredEditor;
            }
            catch
            {
                // Swallow.
            }

            return returnValue;
        }
    }
}
