// <copyright file="LaunchModelUpdater.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Infrastructure.Parsers
{
    using Domain.Models.Configuration;
    using Logic.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    /// <summary>
    /// Updates a LaunchModel with command line switches.
    /// </summary>
    public class LaunchModelUpdater
    {
        private readonly CommandLineArgument[] args;
        private readonly LaunchModel launchModel;
        private readonly List<string> services = new();
        private readonly List<string> executables = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchModelUpdater"/> class.
        /// </summary>
        /// <param name="args">The arguments.</param>
        private LaunchModelUpdater(CommandLineArgument[] args, LaunchModel launchModel)
        {
            this.args = args;
            this.launchModel = launchModel;
        }

        /// <summary>
        /// Parses the specified arguments.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <param name="launchModel">The launch model.</param>
        public static void UpdateWithCommandLineArguments(CommandLineArgument[] arguments, LaunchModel launchModel)
        {
            var updater = new LaunchModelUpdater(arguments, launchModel);
            updater.Parse();
        }

        private static void CheckForSingleOption(string[] options, Logic.SwitchCommands switchCommand)
        {
            if (options.Length == 0)
            {
                throw new NotSupportedException($"No options provided for switch command '{switchCommand.GetCommandLineArgument()}'");
            }

            if (options.Length > 1)
            {
                throw new NotSupportedException($"You can only specify one options for command '{switchCommand.GetCommandLineArgument()}");
            }
        }

        private void Parse()
        {
            foreach (var arg in this.args)
            {
                this.ParseCommandLineArgument(this.launchModel, arg);
            }

            var allServices = this.launchModel.Services.ToList();
            var allExecutables = this.launchModel.Executables.ToList();

            allServices.AddRange(this.services);
            allExecutables.AddRange(this.executables);

            this.launchModel.Services = allServices.ToArray();
            this.launchModel.Executables = allExecutables.ToArray();
        }

        private void ParseCommandLineArgument(LaunchModel returnValue, CommandLineArgument commandLineArgument)
        {
            var switchCommand = commandLineArgument.Command.GetSwitchCommand();
            var options = commandLineArgument.Options;
            switch (switchCommand)
            {
                case Logic.SwitchCommands.Unknown:
                    return;
                case Logic.SwitchCommands.Run:
                    CheckForSingleOption(options, switchCommand);
                    returnValue.ExecutableToLaunch = options[0];
                    return;
                case Logic.SwitchCommands.Edit:
                    returnValue.EditConfiguration = true;
                    return;

                case Logic.SwitchCommands.UseBatch:
                    returnValue.UseBatchFile = true;
                    return;

                case Logic.SwitchCommands.ShutdownExplorer:
                    returnValue.ShutdownExplorer = true;
                    return;

                case Logic.SwitchCommands.Priority:

                    CheckForSingleOption(options, switchCommand);

                    if (Enum.TryParse(options[0], true, out ProcessPriorityClass processPriorityClass))
                    {
                        returnValue.Priority = processPriorityClass;
                    }

                    return;

                case Logic.SwitchCommands.Services:

                    this.services.AddRange(options);

                    return;

                case Logic.SwitchCommands.Executables:
                    this.executables.AddRange(options);

                    return;
            }
        }
    }
}
