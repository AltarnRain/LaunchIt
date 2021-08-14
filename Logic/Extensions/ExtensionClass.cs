// <copyright file="ExtensionClass.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Logic.Extensions
{
    using Logic.Contracts.Providers;
    using System;

    /// <summary>
    /// Class that provides various extension methods.
    /// </summary>
    public static class ExtensionClass
    {
        /// <summary>
        /// Gets the action file path.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <returns>Location of the actions.txt file.</returns>
        public static string ConfigurationFile(this IPathProvider self)
        {
            return self.MapPath("~/LaunchIt.yml");
        }

        /// <summary>
        /// Gets the switch command.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <returns>The switch command.</returns>
        public static SwitchCommands GetSwitchCommand(this string self)
        {
            if (Enum.TryParse(self, true, out SwitchCommands result))
            {
                return result;
            }

            return SwitchCommands.Unknown;
        }

        /// <summary>
        /// Gets the command line argument.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <returns>Command line argument for a switch command.</returns>
        public static string GetCommandLineArgument(this SwitchCommands self)
        {
            return $"-{self.ToString().ToLower()}";
        }
    }
}