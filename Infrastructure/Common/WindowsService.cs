using Domain.Models.Services;
using Logic.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.ServiceProcess;

namespace Infrastructure.Common
{

    [SupportedOSPlatform("Windows")]
    public class WindowsService : Logic.Common.IWindowServices
    {
        private readonly IUnstoppableServiceProvider unstoppableServiceProvider;

        public WindowsService(IUnstoppableServiceProvider unstoppableServiceProvider)
        {
            this.unstoppableServiceProvider = unstoppableServiceProvider;
        }
        
        public IEnumerable<ServiceModel> GetServices()
        {
            var services = ServiceController.GetServices();

            var unstoppableServices = this.unstoppableServiceProvider.GetUnstoppableServices();

            foreach(var service in services)
            {
                yield return new ServiceModel
                {
                    ServiceName = service.ServiceName,
                    DisplayName = service.DisplayName,
                    Running = service.Status == ServiceControllerStatus.Running,
                    Enabled = service.StartType != ServiceStartMode.Disabled,
                    CanStop = service.CanStop,
                    CanPauseAndContinue = service.CanPauseAndContinue,
                    CanShutdown = service.CanShutdown,
                    Ignored = unstoppableServices.Contains(service.ServiceName),
                };
            }
        }

        public bool Start(ServiceModel service)
        {
            return DoActionOnService(service, (actualService) => actualService.Stop());
        }

        public bool Stop(ServiceModel service)
        {
            return DoActionOnService(service, (actualService) => actualService.Stop());
        }

        /// <summary>
        /// Does the action.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="action">The action.</param>
        /// <returns>True if executed, false otherwise.</returns>
        private static bool DoActionOnService(ServiceModel service, Action<ServiceController> action)
        {
            var actualService = GetActualService(service);
            if (actualService is null)
            {
                return false;
            }

            action(actualService);
            return true;
        }

        private static ServiceController? GetActualService(ServiceModel service)
        {
            return ServiceController.GetServices().SingleOrDefault(s => s.ServiceName == service.ServiceName);
        }
    }
}
