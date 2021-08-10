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
        private readonly string[] args;
        private readonly LaunchModel launchModel;
        private readonly List<string> services = new();
        private readonly List<string> executables = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchModelUpdater"/> class.
        /// </summary>
        /// <param name="args">The arguments.</param>
        private LaunchModelUpdater(string[] args, LaunchModel launchModel)
        {
            this.args = args;
            this.launchModel = launchModel;
        }

        /// <summary>
        /// Parses the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="launchModel">The launch model.</param>
        public static void UpdateWithCommandLineArguments(string[] args, LaunchModel launchModel)
        {
            // Nothing to do.
            if (args.Length == 0)
            {
                return;
            }

            var parser = new LaunchModelUpdater(args, launchModel);
            parser.Parse();
        }

        private void Parse()
        {
            for (int i = 0; i < this.args.Length; i++)
            {
                var currentArgument = this.args[i];
                var nextArgument = (i + 1) < this.args.Length ? this.args[i + 1] : null;
                this.UpdateCommandLineParserResult(this.launchModel, currentArgument, nextArgument);
            }

            var allServices = this.launchModel.Services.ToList();
            var allExecutables = this.launchModel.Executables.ToList();

            allServices.AddRange(this.services);
            allExecutables.AddRange(this.executables);

            this.launchModel.Services = allServices.ToArray();
            this.launchModel.Executables = allExecutables.ToArray();
        }

        private void UpdateCommandLineParserResult(LaunchModel returnValue, string currentArgument, string? nextArgument)
        {
            if (currentArgument.IsSwitchCommand())
            {
                this.ParseSwitchCommand(returnValue, currentArgument, nextArgument);
                return;
            }

            returnValue.ExecutableToLaunch = currentArgument;
        }

        private void ParseSwitchCommand(LaunchModel returnValue, string currentArgument, string? nextArgument)
        {
            var switchCommand = currentArgument.GetSwitchCommand();
            this.ParseSwitchCommand(returnValue, nextArgument, switchCommand);
        }

        private void ParseSwitchCommand(LaunchModel returnValue, string? nextArgument, Logic.SwitchCommands switchCommand)
        {
            switch (switchCommand)
            {
                case Logic.SwitchCommands.Unknown:
                    return;
                case Logic.SwitchCommands.Run:
                    returnValue.ExecutableToLaunch = nextArgument ?? "cmd";
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
                    if (Enum.TryParse(nextArgument, true, out ProcessPriorityClass processPriorityClass))
                    {
                        returnValue.Priority = processPriorityClass;
                    }

                    return;

                case Logic.SwitchCommands.ShutdownService:
                    if (nextArgument is null)
                    {
                        return;
                    }

                    this.services.Add(nextArgument);

                    return;

                case Logic.SwitchCommands.ShutdownExecutable:
                    if (nextArgument is null)
                    {
                        return;
                    }

                    this.executables.Add(nextArgument);

                    return;
            }
        }
    }
}
