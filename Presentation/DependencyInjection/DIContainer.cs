using StrongInject;

namespace GameOptimizer.DependencyInjection
{

    [Register(typeof(Logic.Startup), Scope.SingleInstance)]
    [Register(typeof(Infrastructure.Common.WindowsService), Scope.SingleInstance, typeof(Logic.Common.IWindowServices))]
    [Register(typeof(Infrastructure.Common.ConsoleLogService), Scope.SingleInstance, typeof(Logic.Common.ILogger))]
    [Register(typeof(Infrastructure.Providers.UnstoppableServicesProvider), Scope.SingleInstance, typeof(Logic.Providers.IUnstoppableServiceProvider))]
    [Register(typeof(Infrastructure.Providers.PathProvider), Scope.SingleInstance, typeof(Logic.Providers.IPathProvider))]

    #region Class definition. No need to see this.
    public partial class DIContainer : IContainer<Logic.Startup> 
    {
        // Code is generated. Do not write implementation here.
    }
    #endregion
}
