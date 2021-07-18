using Domain.Models.Services;
using System;
using System.Collections.Generic;

namespace Infrastructure.Common
{
    public class WindowsServiceProvider : Logic.Common.IWindowServicesProvider
    {
        public IEnumerable<ServiceModel> GetServices()
        {
            throw new NotImplementedException();
        }
    }
}
