using Logic.Common;
using Logic.Providers;
using System;
using System.IO;

namespace Infrastructure.Providers
{
    public class UnstoppableServicesProvider : IUnstoppableServiceProvider
    {
        private readonly IPathProvider pathProvider;

        public UnstoppableServicesProvider(IPathProvider pathProvider)
        {
            this.pathProvider = pathProvider;
        }

        public string[] GetUnstoppableServices()
        {
            var ignoredServicesFile = this.pathProvider.MapPath("~/ignoredservices.dat");

            if (!File.Exists(ignoredServicesFile))
            {
                return Array.Empty<string>();
            }

            return File.ReadAllLines(ignoredServicesFile);
        }
    }
}
