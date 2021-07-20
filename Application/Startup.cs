using Logic.Common;
using System.Diagnostics;
using System.Linq;

namespace Logic
{
    public class Startup
    {
        private readonly IWindowServices windowServices;
        private readonly ILogger logger;

        public Startup(IWindowServices windowServicesProvider, ILogger logger)
        {
            this.windowServices = windowServicesProvider;
            this.logger = logger;
        }

        public void Start()
        {
            var services = this.windowServices
                .GetServices()
                .Where(s => s.Enabled && s.Running && s.CanShutdown);

            foreach(var service in services)
            {
                this.logger.Push(service.DisplayName, service.ServiceName, service.Running, service.Enabled, service.CanPauseAndContinue, service.CanShutdown, service.CanStop, service.Ignored);
            }

            var logLines = this.logger.Get(new[] {"Display name", "Service name", "Running", "Enabled", "Can pause and continue", "Can shutdown", "Can stop", "Ignored" });

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
