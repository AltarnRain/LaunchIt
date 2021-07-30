// <copyright file="CommandLineParser.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Parsers
{
    using Domain.Models.Configuration;
    using Logic.Extensions;
    using System;
    using System.Diagnostics;
    using System.Linq;

    /// <summary>
    /// Parses command line arguments.
    /// </summary>
    public static class CommandLineParser
    {
        /// <summary>
        /// Parses the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>A LaunchModel.</returns>
        public static CommandLineParserResult Parse(string[] args)
        {
            var returnValue = new CommandLineParserResult();

            // Nothing to do.
            if (args.Length == 0)
            {
                return returnValue;
            }

            for (int i = 0; i < args.Length; i++)
            {
                var currentArgument = args[i];
                var nextArgument = (i + 1) < args.Length ? args[i + 1] : null;
                UpdateCommandLineParserResult(returnValue, currentArgument, nextArgument);
            }

            return returnValue;
        }

        private static void UpdateCommandLineParserResult(CommandLineParserResult returnValue, string currentArgument, string? nextArgument)
        {
            if (currentArgument.IsSwitchCommand())
            {
                ParseSwitchCommand(returnValue, currentArgument, nextArgument);
                return;
            }

            returnValue.LaunchModel.ExecutableToLaunch = currentArgument;
        }

        private static void ParseSwitchCommand(CommandLineParserResult returnValue, string currentArgument, string? nextArgument)
        {
            var switchCommand = currentArgument.GetSwitchCommand();
            switch (switchCommand)
            {
                case Logic.SwitchCommands.Unknown:
                    break;
                case Logic.SwitchCommands.Reset:
                    returnValue.ResetConfiguration = true;
                    break;
                case Logic.SwitchCommands.Edit:
                    returnValue.EditConfiguration = true;
                    break;
                case Logic.SwitchCommands.UseBatch:
                    returnValue.LaunchModel.UseBatchFile = true;
                    break;
                case Logic.SwitchCommands.Priority:
                    if (Enum.TryParse(nextArgument, true, out ProcessPriorityClass processPriorityClass))
                    {
                        returnValue.LaunchModel.Priority = processPriorityClass;
                    }

                    break;

                case Logic.SwitchCommands.MonitorRestarts:

                    if (bool.TryParse(nextArgument, out bool monitorRestarts))
                    {
                        returnValue.LaunchModel.MonitoringConfiguration.MonitorRestarts = monitorRestarts;
                    }

                    break;
                case Logic.SwitchCommands.MonitorInterval:

                    if (int.TryParse(nextArgument, out int monitorInterval))
                    {
                        returnValue.LaunchModel.MonitoringConfiguration.MonitoringInterval = monitorInterval;
                    }

                    break;
            }
        }
    }
}
