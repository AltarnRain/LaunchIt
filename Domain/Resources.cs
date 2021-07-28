// <copyright file="Resources.cs" company="Antonio Invernizzi V">
// Copyright (c) Antonio Invernizzi V. All rights reserved.
// </copyright>

namespace Domain
{
    /// <summary>
    /// Helper class for obtaining resources.
    /// </summary>
    public static class Resources
    {
        private const string UsageManifest = "Domain...Help.Usage.txt";

        /// <summary>
        /// Gets the help file.
        /// </summary>
        /// <returns>Content of Usage.txt.</returns>
        public static string Usage()
        {
            return GetStringContent(UsageManifest);
        }

        private static string GetStringContent(string manifest)
        {
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var resource = assembly.GetManifestResourceStream(manifest);
            if (resource is null)
            {
                return string.Empty;
            }

            resource.Seek(0, System.IO.SeekOrigin.Begin);
            var byteContent = new byte[resource.Length];
            resource.Read(byteContent, 0, (int)resource.Length);

            var content = System.Text.Encoding.UTF8.GetString(byteContent);
            return content;
        }
    }
}
