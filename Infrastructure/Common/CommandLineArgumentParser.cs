// <copyright file="CommandLineArgumentParser.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Common
{
    using Domain.Models.Parsing;
    using Logic.Common;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Parses command line arguments.
    /// </summary>
    /// <seealso cref="Logic.Common.ICommandLineArgumentParser" />
    public class CommandLineArgumentParser : ICommandLineArgumentParser
    {
        /// <summary>
        /// Parses the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>
        /// A collection of CommandLineArgumentModels.
        /// </returns>
        public CommandLineParseResult Parse(string[] args)
        {
            var commandLineArgumentInfos = args.Select(a => new CommandLineArgumentInfo(a)).ToArray();
            var errorMessages = new List<string>();

            CheckForUnknownArguments(commandLineArgumentInfos, errorMessages);
            CheckForDuplicateArguments(commandLineArgumentInfos, errorMessages);

            return new CommandLineParseResult
            {
                Succes = errorMessages.Count == 0,
                ErrorMessages = errorMessages.ToArray(),
                CommandLineArgumentInfos = commandLineArgumentInfos,
            };
        }

        private static void CheckForDuplicateArguments(CommandLineArgumentInfo[] info, List<string> messages)
        {
            var duplicates = from i in info
                             group i by i.ArgumentType into g
                             select g;

            var duplicateArguments = new List<string>();
            foreach (var duplicate in duplicates)
            {
                if (duplicate.Count() > 1)
                {
                    foreach (var x in duplicate)
                    {
                        duplicateArguments.Add(x.Argument);
                    }
                }
            }

            if (duplicateArguments.Count > 0)
            {
                messages.Add($"You used a command line switch multiple times. That doesn't work. '{string.Join(",", duplicateArguments)}'");
            }
        }

        private static void CheckForUnknownArguments(CommandLineArgumentInfo[] info, List<string> messages)
        {
            var unknowns = info
                            .Where(i => i.ArgumentType == CommandLineArgumentType.Unknown)
                            .Select(i => i.Argument)
                            .ToArray();

            if (unknowns.Any())
            {
                messages.Add($"You used one or multiple command line argument(s) I don't know. '{string.Join(",", unknowns)}'");
            }
        }
    }
}
