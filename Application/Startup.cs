using Logic.Common;

namespace Logic
{
    public class Startup
    {
        private readonly IWindowServicesProvider windowServicesProvider;
        private readonly ILogger logger;

        public Startup(IWindowServicesProvider windowServicesProvider, ILogger logger)
        {
            this.windowServicesProvider = windowServicesProvider;
            this.logger = logger;
        }

        public void Start()
        {
            var services = this.windowServicesProvider.GetServices();

            foreach(var service in services)
            {
                this.logger.Log(service.DisplayName);
            }
        }
    }
}
