// <copyright file="ActionFileWriter.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Domain
{
    using System.IO;

    /// <summary>
    /// Stream writer class for an Action file.
    /// </summary>
    /// <seealso cref="System.IO.StreamWriter" />
    public class ActionFileWriter : StreamWriter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActionFileWriter"/> class.
        /// </summary>
        /// <param name="path">The complete file path to write to. <paramref name="path" /> can be a file name.</param>
        public ActionFileWriter(string path)
            : base(path)
        {
        }

        /// <summary>
        /// Adds the comment.
        /// </summary>
        /// <param name="comment">The comment.</param>
        public void WriteComment(string comment)
        {
            this.WriteLine($"# {comment}");
        }

        /// <summary>
        /// Writes the service section.
        /// </summary>
        public void WriteServiceSection()
        {
            this.WriteLine(Constants.Sections.Services);
        }

        /// <summary>
        /// Writes the service section.
        /// </summary>
        public void WriteExecutableSection()
        {
            this.WriteLine(Constants.Sections.Executables);
        }
    }
}
