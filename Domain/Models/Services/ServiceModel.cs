namespace Domain.Models.Services
{
    public class ServiceModel
    {
        public string ServiceName { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public bool Running { get; set; }
        public bool Enabled { get; set; }
        public bool CanStop { get; set; }
        public bool CanPauseAndContinue { get; set; }
        public bool CanShutdown { get; set; }
        public bool Ignored { get; set; }
    }
}
