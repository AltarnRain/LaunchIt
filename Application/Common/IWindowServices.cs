using Domain.Models.Services;
using System.Collections.Generic;

namespace Logic.Common
{
    public interface IWindowServices
    {
        IEnumerable<ServiceModel> GetServices();
        bool Start(ServiceModel service);
        bool Stop(ServiceModel service);
    }
}
