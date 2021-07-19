using System.Collections.Generic;
using System.Linq;

namespace Infrastructure
{
    public static class ExtensionClass
    {
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
