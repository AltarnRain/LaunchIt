// <copyright file="ExtensionClass.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Extensions
{
    using Logic;
    using System.Linq;

    /// <summary>
    /// Extension class.
    /// </summary>
    public static partial class ExtensionClass
    {
        /// <summary>
        /// Gets the configuration file.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The configuration file.</returns>
        public static string GetConfigurationFile(this Domain.Models.Configuration.CommandLineArgument[] arguments)
        {
            var configFileArgument = arguments.FirstOrDefault(a => a.Command.Equals(SwitchCommands.Edit.ToString(), System.StringComparison.OrdinalIgnoreCase));
            var configFile = Domain.Constants.ConfigurationConstants.DefaultConfigurationFileName;

            if (configFileArgument is not null && configFileArgument.Options.Length > 0)
            {
                configFile = configFileArgument.Options[0];
            }

            return configFile;
        }
    }
}
