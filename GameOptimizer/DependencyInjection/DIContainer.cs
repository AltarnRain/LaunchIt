using StrongInject;

namespace GameOptimizer.DependencyInjection
{

    [Register(typeof(Logic.Startup), Scope.SingleInstance)]
    [Register(typeof(Infrastructure.Common.WindowsServiceProvider), Scope.SingleInstance, typeof(Logic.Common.IWindowServicesProvider))]
    [Register(typeof(Infrastructure.Common.ConsoleLogService), Scope.SingleInstance, typeof(Logic.Common.ILogger))]

    #region Class definition. No need to see this.
    public partial class DIContainer : IContainer<Logic.Startup> 
    {
        // Code is generated. Do not write implementation here.
    }
    #endregion
}
