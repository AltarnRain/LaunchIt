// <copyright file="EditorService.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Services
{
    using Infrastructure.Helpers;
    using Logic.Contracts.Services;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Service the provides editing of a file.
    /// </summary>
    [ExcludeFromCodeCoverage(Justification = "The only thing this class does is pass stuff to the ProcessWrapper. I am not mocking that.")]
    public class EditorService : IEditorService
    {
        private readonly IConfigurationService configurationService;
        private readonly IProcessWrapper processWrapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditorService" /> class.
        /// </summary>
        /// <param name="configurationService">The configuration service.</param>
        /// <param name="processWrapper">The process wrapper.</param>
        public EditorService(IConfigurationService configurationService, IProcessWrapper processWrapper)
        {
            this.configurationService = configurationService;
            this.processWrapper = processWrapper;
        }

        /// <summary>
        /// Edits the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        public void Edit(string file)
        {
            this.processWrapper.Start(this.GetEditor(), file)?.WaitForExit();
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
