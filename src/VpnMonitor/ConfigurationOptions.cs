namespace VpnMonitor
{
    public class ConfigurationOptions
    {
        public string VpnName { get; set; }
        public bool ShowNotification { get; set; } = true;
        public int PollingIntervalMs { get; set; } = 500;
    }
}