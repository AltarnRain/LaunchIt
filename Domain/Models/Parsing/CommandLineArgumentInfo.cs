// <copyright file="CommandLineArgumentInfo.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Domain.Models.Parsing
{
    /// <summary>
    /// Model that represents a command line argument.
    /// </summary>
    public record CommandLineArgumentInfo
    {
        private readonly string argument;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandLineArgumentInfo"/> class.
        /// </summary>
        /// <param name="argument">The argument.</param>
        public CommandLineArgumentInfo(string argument)
        {
            this.argument = argument;
        }

        /// <summary>
        /// Gets the argument.
        /// </summary>
        public string Argument => this.argument;

        /// <summary>
        /// Gets the type of the argument.
        /// </summary>
        /// <value>
        /// The type of the argument.
        /// </value>
        public CommandLineArgumentType ArgumentType
        {
            get
            {
                if (this.argument.StartsWith(Domain.Constants.CommandLineSwitches.Initialize))
                {
                    return CommandLineArgumentType.Initialize;
                }

                if (this.argument.StartsWith(Domain.Constants.CommandLineSwitches.NoExplorer))
                {
                    return CommandLineArgumentType.NoExplorer;
                }

                if (this.argument.StartsWith(Domain.Constants.CommandLineSwitches.Priority))
                {
                    return CommandLineArgumentType.Priority;
                }

                if (this.argument.StartsWith(Domain.Constants.CommandLineSwitches.RunGame))
                {
                    return CommandLineArgumentType.RunGame;
                }

                return CommandLineArgumentType.Unknown;
            }
        }

        /// <summary>
        /// Gets the value. This is for command line switches that let to specify something else e.g. /arg=1.
        /// </summary>
        public string? Value
        {
            get
            {
                var splitterPosition = this.argument.IndexOf('=');

                if (splitterPosition == -1)
                {
                    return null;
                }

                // Add 1 to skil = sign.
                splitterPosition++;

                return this.argument[splitterPosition..].Trim();
            }
        }
    }
}
