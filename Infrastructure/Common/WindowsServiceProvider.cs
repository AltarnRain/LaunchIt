using Domain.Models.Services;
using System;
using System.Collections.Generic;
using System.Runtime.Versioning;
using System.ServiceProcess;

namespace Infrastructure.Common
{
    public class WindowsServiceProvider : Logic.Common.IWindowServicesProvider
    {
        [SupportedOSPlatform("Windows")]
        public IEnumerable<ServiceModel> GetServices()
        {
            var services = ServiceController.GetServices();

            foreach(var service in services)
            {
                yield return new ServiceModel
                {
                    ServiceName = service.ServiceName,
                    DisplayName = service.DisplayName,
                    Running = service.Status == ServiceControllerStatus.Running,
                    Enabled = service.StartType != ServiceStartMode.Disabled,
                };
            }
        }
    }
}
