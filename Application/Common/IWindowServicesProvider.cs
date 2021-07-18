using Domain.Models.Services;
using System.Collections.Generic;

namespace Logic.Common
{
    public interface IWindowServicesProvider
    {
        IEnumerable<ServiceModel> GetServices();
    }
}
