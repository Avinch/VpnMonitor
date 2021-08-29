namespace VpnMonitor
{
    public class ConfigurationOptions
    {
        public string VpnName { get; set; }
        public bool ShowConnectedNotification { get; set; } = true;
        public int PollingIntervalMs { get; set; } = 500;
        public bool ShowReminderNotification { get; set; } = false;
        public double ReminderIntervalMin { get; set; } = 60;
    }
}