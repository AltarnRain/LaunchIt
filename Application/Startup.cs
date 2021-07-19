using Logic.Common;
using System.Diagnostics;
using System.Linq;

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
            var services = this.windowServicesProvider
                .GetServices()
                .Where(s => s.Enabled && s.Running);

            foreach(var service in services)
            {
                this.logger.Push(service.DisplayName, service.ServiceName, service.Running, service.Enabled);
            }

            var logLines = this.logger.Get(new[] {"Display name", "Service name", "Running", "Enabled" });

            // Create a temp file name and path and add the .txt extension so the file can be opened using the default text editor.
            var tempFile = System.IO.Path.GetTempFileName() + ".txt";

            System.IO.File.WriteAllLines(tempFile, logLines);

            // Needed to execute in shell context.
            var processInfo = new ProcessStartInfo
            {
                FileName = tempFile,
                UseShellExecute = true,
            };

            Process.Start(processInfo);
        }
    }
}
