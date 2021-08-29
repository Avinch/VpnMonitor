using System.Linq;
using System.Net.NetworkInformation;
using Microsoft.Extensions.Configuration;

namespace VpnMonitor
{
    public class VpnService
    {
        private bool _vpnLoaded;
        private readonly ConfigurationOptions _config;
        private readonly ReminderService _reminder;
        public bool Connected => CheckConnection() && VpnNetworkInterface.OperationalStatus == OperationalStatus.Up;

        private ConnectedState _currentState;
        
        public NetworkInterface VpnNetworkInterface { get; private set; }
        private TrayIconProvider _tray { get; set; }
        
        public VpnService(ConfigurationOptions config, TrayIconProvider tray, ReminderService reminder)
        {
            _config = config;
            _reminder = reminder;
            _tray = tray;
            CheckConnection();
        }
        
        private bool CheckConnection()
        {
            var obj = NetworkInterface.GetAllNetworkInterfaces()
                .FirstOrDefault(x =>
                    x.Description.ToLower().Contains(_config.VpnName));

            if (obj == null) return false;
            
            _vpnLoaded = true;
            VpnNetworkInterface = obj;

            return _vpnLoaded;
        }

        public void CheckVpnStatus()
        {
            var oldState = _currentState;
            var newState = Connected ? ConnectedState.Connected : ConnectedState.Disconnected;

            _currentState = newState;
            
            if (oldState != newState)
            {
                _tray.SetIconConnectionState(Connected, VpnNetworkInterface?.Description);

                if (_config.ShowConnectedNotification)
                {
                    _tray.SendConnectionStateChangeNotification(Connected, VpnNetworkInterface?.Description);
                }

                if (_config.ShowReminderNotification)
                {
                    if (newState == ConnectedState.Connected)
                    {
                        _reminder.Start();
                    }
                    else
                    {
                        _reminder.Stop();
                    }
                }
            }
        }
    }
}