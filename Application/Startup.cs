using Logic.Common;
using System;

namespace Logic
{
    public class Startup
    {
        private readonly IWindowServicesProvider windowServicesProvider;

        public Startup(Common.IWindowServicesProvider windowServicesProvider)
        {
            this.windowServicesProvider = windowServicesProvider;
        }

        public void Start()
        {
            Console.WriteLine("Started application");
        }
    }
}
