// <copyright file="Priotity.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Domain.Constants
{
    /// <summary>
    /// Constant class for priorities.
    /// </summary>
    public static class Priotity
    {
        /// <summary>
        /// The normal.
        /// </summary>
        public const string Normal = "normal";

        /// <summary>
        /// The abovenormal.
        /// </summary>
        public const string Abovenormal = "abovenormal";

        /// <summary>
        /// The high.
        /// </summary>
        public const string High = "high";

        /// <summary>
        /// Gets the valid prorities.
        /// </summary>
        public static string[] ValidProrities => new[] { Normal, Abovenormal, High };
    }
}
