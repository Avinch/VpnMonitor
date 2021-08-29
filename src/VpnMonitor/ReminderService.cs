using System.Timers;

namespace VpnMonitor
{
    public class ReminderService
    {
        private readonly TrayIconProvider _tray;
        private readonly ConfigurationOptions _config;
        
        private Timer _reminderTimer;
        
        public ReminderService(ConfigurationOptions config, TrayIconProvider tray)
        {
            _tray = tray;
            _config = config;
        }
        
        public void Start()
        {
            if (_reminderTimer == null)
            {
                _reminderTimer = new Timer( (_config.ReminderIntervalMin * 60) * 1000);
                _reminderTimer.Elapsed += OnReminderTimerElapsed;
                _reminderTimer.AutoReset = true;
            }
            _reminderTimer.Enabled = true;
        }

        private void OnReminderTimerElapsed(object sender, ElapsedEventArgs e)
        {
            _tray.SendConnectedReminderNotification();
        }

        public void Stop()
        {
            if (_reminderTimer == null) return;
            _reminderTimer.Enabled = false;
        }
    }
}