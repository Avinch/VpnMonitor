using System;
using System.Threading;
using System.Timers;
using System.Windows.Forms;

namespace VpnMonitor
{
    public class Startup
    {
        private readonly TrayIconProvider _trayIconProvider;
        private readonly ConfigurationOptions _config;
        
        public Startup(TrayIconProvider tray, ConfigurationOptions config)
        {
            _trayIconProvider = tray;
            _config = config;
        }
        
        public void Start()
        {
            var mainThread = new Thread(
                delegate ()
                {
                    _trayIconProvider.CreateTrayIcon();
                    Application.Run();
                });
            
            mainThread.Start();
            
            var timer = new System.Timers.Timer(_config.PollingIntervalMs);
            timer.Elapsed += OnTimerElapsed;
            timer.AutoReset = true;
            timer.Enabled = true;
        }
        
        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                _trayIconProvider.CheckVpn();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}