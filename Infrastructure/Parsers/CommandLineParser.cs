// <copyright file="CommandLineParser.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Parsers
{
    using Logic.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    /// <summary>
    /// Parses command line arguments.
    /// </summary>
    public class CommandLineParser
    {
        private readonly string[] args;
        private List<string> services = new();
        private List<string> executables = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandLineParser"/> class.
        /// </summary>
        /// <param name="args">The arguments.</param>
        private CommandLineParser(string[] args)
        {
            this.args = args;
        }

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

            var parser = new CommandLineParser(args);
            returnValue = parser.Parse();

            return returnValue;
        }

        private CommandLineParserResult Parse()
        {
            var returnValue = new CommandLineParserResult();
            for (int i = 0; i < this.args.Length; i++)
            {
                var currentArgument = this.args[i];
                var nextArgument = (i + 1) < this.args.Length ? this.args[i + 1] : null;
                this.UpdateCommandLineParserResult(returnValue, currentArgument, nextArgument);
            }

            returnValue.LaunchModel.Services = this.services.ToArray();
            returnValue.LaunchModel.Executables = this.executables.ToArray();

            return returnValue;
        }

        private void UpdateCommandLineParserResult(CommandLineParserResult returnValue, string currentArgument, string? nextArgument)
        {
            if (currentArgument.IsSwitchCommand())
            {
                this.ParseSwitchCommand(returnValue, currentArgument, nextArgument);
                return;
            }

            returnValue.LaunchModel.ExecutableToLaunch = currentArgument;
        }

        private void ParseSwitchCommand(CommandLineParserResult returnValue, string currentArgument, string? nextArgument)
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

                case Logic.SwitchCommands.ServiceShutdownAfterRestart:

                    if (bool.TryParse(nextArgument, out bool serviceShutdownAfterRestart))
                    {
                        returnValue.LaunchModel.ServiceShutdownConfiguration.ShutdownAfterRestart = serviceShutdownAfterRestart;
                    }

                    break;

                case Logic.SwitchCommands.ServiceShutdownOnlyConfigured:

                    if (bool.TryParse(nextArgument, out bool serviceShutdownOnlyConfigured))
                    {
                        returnValue.LaunchModel.ServiceShutdownConfiguration.OnlyConfigured = serviceShutdownOnlyConfigured;
                    }

                    break;

                case Logic.SwitchCommands.ServiceShutdownMaximumAttempts:

                    if (int.TryParse(nextArgument, out int serviceShutdownMaximumAttempts))
                    {
                        returnValue.LaunchModel.ServiceShutdownConfiguration.MaximumShutdownAttempts = serviceShutdownMaximumAttempts;
                    }

                    break;

                case Logic.SwitchCommands.ExecutableShutdownAfterRestart:

                    if (bool.TryParse(nextArgument, out bool executableShutdownAfterRestart))
                    {
                        returnValue.LaunchModel.ExecutableShutdownConfiguration.ShutdownAfterRestart = executableShutdownAfterRestart;
                    }

                    break;

                case Logic.SwitchCommands.ExecutableShutdownOnlyConfigured:

                    if (bool.TryParse(nextArgument, out bool executableShutdownOnlyConfigured))
                    {
                        returnValue.LaunchModel.ExecutableShutdownConfiguration.OnlyConfigured = executableShutdownOnlyConfigured;
                    }

                    break;

                case Logic.SwitchCommands.ExecutableShutdownMaximumAttempts:

                    if (int.TryParse(nextArgument, out int executableShutdownMaximumAttempts))
                    {
                        returnValue.LaunchModel.ExecutableShutdownConfiguration.MaximumShutdownAttempts = executableShutdownMaximumAttempts;
                    }

                    break;

                case Logic.SwitchCommands.ShutdownService:
                    if (nextArgument is null)
                    {
                        return;
                    }

                    this.services.Add(nextArgument);

                    break;

                case Logic.SwitchCommands.ShutdownExecutable:
                    if (nextArgument is null)
                    {
                        return;
                    }

                    this.executables.Add(nextArgument);

                    break;
            }
        }
    }
}
