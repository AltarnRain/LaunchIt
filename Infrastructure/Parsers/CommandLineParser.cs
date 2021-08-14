// <copyright file="CommandLineParser.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Parsers
{
    using Domain.Models.Configuration;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Parse command line arguments to CommandLineArgument objects.
    /// </summary>
    public static class CommandLineParser
    {
        /// <summary>
        /// Parses the specified arguments.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <returns>Collection of CommandLine Argument models.</returns>
        public static CommandLineArgument[] Parse(string[] arguments)
        {
            if (arguments.Length == 0)
            {
                return Array.Empty<CommandLineArgument>();
            }

            var fullArgument = string.Join(" ", arguments);

            return Parse(fullArgument);
        }

        /// <summary>
        /// Parses the specified argument.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <returns>CommandLineArgument collection.</returns>
        public static CommandLineArgument[] Parse(string argument)
        {
            // Nothing to do. Bye!
            if (string.IsNullOrWhiteSpace(argument))
            {
                return Array.Empty<CommandLineArgument>();
            }

            var parsedArguments = new List<CommandLineArgument>();
            var stringToParse = argument;

            // Loop over the string to parse until it's empty. Each time
            // something is identified as command line argument it'll be
            // removed from stringToParse.
            // If nothing is identified to first character is skipped, removed and we move on to the next character.
            while (stringToParse.Length > 0)
            {
                var c = stringToParse[0];

                // Its a switch.
                if (c == '-' || c == '/')
                {
                    stringToParse = ParseSwitch(parsedArguments, stringToParse);
                }
                else if (c == '=')
                {
                    stringToParse = ParseOptions(parsedArguments, stringToParse);
                }
                else
                {
                    // Unidentified character so we move on to the next character
                    stringToParse = stringToParse[1..];
                }
            }

            return parsedArguments.ToArray();
        }

        private static string ParseOptions(List<CommandLineArgument> parsedArguments, string stringToParse)
        {
            var options = Array.Empty<string>();

            // Remove '='
            stringToParse = stringToParse[1..];

            var nextSwitch = stringToParse.IndexOf('-');

            if (nextSwitch > -1)
            {
                // We have a position, now substring on the lowest value.
                var optionString = stringToParse.Substring(0, nextSwitch);
                options = optionString.Split(',').Select(s => s.Trim()).ToArray();
                stringToParse = stringToParse[nextSwitch..];
            }
            else
            {
                options = stringToParse.Split(',').Select(s => s.Trim()).ToArray();
                stringToParse = string.Empty;
            }

            // We've isolated the options of the previous command line switch.
            parsedArguments.Last().Options = options;
            return stringToParse;
        }

        private static string ParseSwitch(List<CommandLineArgument> parsedArguments, string stringToParse)
        {
            // Remove '-'
            stringToParse = stringToParse[1..];

            var switchCommand = string.Empty;

            // A switch can be followed by three things
            // 1. A new switch, means the current switch has no options.
            // 2. An equal sign to specify switch options.
            var nextSwitch = stringToParse.IndexOf('-');
            var nextEquals = stringToParse.IndexOf('=');

            var take = new[] { nextEquals, nextSwitch }.Where(x => x != -1).ToArray();

            if (take.Any())
            {
                switchCommand = stringToParse.Substring(0, take.Min());
                var position = take.Min();
                stringToParse = stringToParse[position..];
            }
            else
            {
                // End of the line.
                switchCommand = stringToParse;
                stringToParse = string.Empty;
            }

            var commandLineArgument = new CommandLineArgument
            {
                // We've isolated the command. Trim it to deal with trailing spaces.
                Command = switchCommand.Trim(),
            };

            parsedArguments.Add(commandLineArgument);
            return stringToParse;
        }
    }
}