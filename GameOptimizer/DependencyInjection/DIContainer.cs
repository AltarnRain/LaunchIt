using StrongInject;

namespace GameOptimizer.DependencyInjection
{

    [Register(typeof(Logic.Startup), Scope.SingleInstance)]
    [Register(typeof(Infrastructure.Common.WindowsServiceProvider), Scope.SingleInstance, typeof(Logic.Common.IWindowServicesProvider))]
    public partial class DIContainer : IContainer<Logic.Startup>
    {
    }
}
