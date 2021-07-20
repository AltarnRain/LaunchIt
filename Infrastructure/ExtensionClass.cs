// <copyright file="ExtensionClass.cs" company="Onno Invernizzi">
// Copyright (c) Onno Invernizzi. All rights reserved.
// </copyright>

namespace Infrastructure
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Class that provides various extension methods.
    /// </summary>
    public static class ExtensionClass
    {
        /// <summary>
        /// Gets the maximum column sizes.
        /// </summary>
        /// <param name="rows">The rows.</param>
        /// <returns>A dictionary whose key is the column and whose value is the maximum number of characters in said column.</returns>
        public static Dictionary<int, int> GetMaximumColumnSizes(this List<object[]> rows)
        {
            var returnValue = new Dictionary<int, int>();

            foreach (var row in rows)
            {
                var i = 0;
                foreach (var column in row)
                {
                    var columnStringValue = column.ToString() ?? string.Empty;

                    if (!returnValue.ContainsKey(i))
                    {
                        returnValue.Add(i, columnStringValue.Length);
                    }
                    else
                    {
                        if (returnValue[i] < columnStringValue.Length)
                        {
                            returnValue[i] = columnStringValue.Length;
                        }
                    }

                    i++;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Formats the table.
        /// </summary>
        /// <param name="rows">The rows.</param>
        /// <param name="headers">The headers.</param>
        /// <param name="seperationCharacter">The seperation character.</param>
        /// <returns>An array of string formatted for readability.</returns>
        public static string[] FormatTable(this List<object[]> rows, string[]? headers = null, char seperationCharacter = ' ')
        {
            // Create a new list so we can add he headers.
            var table = rows.ToList();

            // We have headers, lets add them.
            if (headers != null)
            {
                table.Insert(0, headers);
            }

            var returnValue = new string[table.Count()];

            // Only pad columns if the seperation character is a space.
            var padColumns = seperationCharacter == ' ';

            var columnSizes = table.GetMaximumColumnSizes();
            var maximumColumnIndex = columnSizes.Count - 1;

            var rowIndex = 0;
            foreach (var row in table)
            {
                var line = string.Empty;
                var columnIndex = 0;

                foreach (var column in row)
                {
                    var columnStringValue = column.ToString() ?? string.Empty;

                    if (padColumns)
                    {
                        line += columnStringValue.PadRight(columnSizes[columnIndex]);
                    }
                    else
                    {
                        line += column;
                    }

                    if (columnIndex < maximumColumnIndex)
                    {
                        line += seperationCharacter;
                    }

                    columnIndex++;
                }

                returnValue[rowIndex] = line;
                rowIndex++;
            }

            return returnValue;
        }
    }
}
