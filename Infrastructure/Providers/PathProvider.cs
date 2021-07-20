using Logic.Providers;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Infrastructure.Providers
{
    public class PathProvider : IPathProvider
    {
        private readonly Dictionary<string, string> pathCache = new();

        public string MapPath(string relativePath)
        {
            if (!relativePath.StartsWith("~"))
            {
                throw new System.Exception("Please pass a relative path prefixed with '~'. Use '~/' to get the root directoty.");
            }

            var strippedPath = relativePath[2..];
            var assemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            if (assemblyLocation is null)
            {
                throw new System.Exception("Could not find the execution directory of the executable");
            }

            var returnValue = Path.Combine(assemblyLocation, strippedPath);

            return returnValue;
        }
    }
}
