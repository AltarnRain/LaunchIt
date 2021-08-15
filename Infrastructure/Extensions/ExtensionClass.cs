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
            var configArgument = arguments.FirstOrDefault(a => a.Command.Equals(SwitchCommands.Config.ToString(), System.StringComparison.OrdinalIgnoreCase));

            var configFile = Domain.Constants.ConfigurationConstants.DefaultConfigurationFileName;

            // Edit over config.
            if (configArgument is not null && configArgument.Options.Length > 0)
            {
                configFile = configArgument.Options[0];

                if (!configFile.EndsWith(Domain.Constants.ConfigurationConstants.ConfigFileExtension))
                {
                    configFile += $".{Domain.Constants.ConfigurationConstants.ConfigFileExtension}";
                }
            }

            return configFile;
        }
    }
}
