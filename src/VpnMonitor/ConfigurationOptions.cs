namespace VpnMonitor
{
    public class ConfigurationOptions
    {
        private string _vpnName;
        public string VpnName { get; set; } = "Vpn";
        public bool ShowNotification { get; set; } = true;
        public int PollingIntervalMs { get; set; } = 2;
    }
}