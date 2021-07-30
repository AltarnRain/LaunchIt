// <copyright file="SwitchCommands.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Logic
{
    /// <summary>
    /// Defines all switch commands like /reset and /edit.
    /// </summary>
    public enum SwitchCommands
    {
        /// <summary>
        /// The unknown
        /// </summary>
        Unknown,

        /// <summary>
        /// The reset configuration
        /// </summary>
        Reset,

        /// <summary>
        /// The edit configuration
        /// </summary>
        Edit,

        /// <summary>
        /// The shutdown explorer
        /// </summary>
        ShutdownExplorer,

        /// <summary>
        /// The use batch
        /// </summary>
        UseBatch,

        /// <summary>
        /// The priority
        /// </summary>
        Priority,

        /// <summary>
        /// The monitor restarts
        /// </summary>
        MonitorRestarts,

        /// <summary>
        /// The monitor interval
        /// </summary>
        MonitorInterval,
    }
}