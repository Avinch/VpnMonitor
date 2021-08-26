using System;
using System.Diagnostics;
using System.Drawing;
using System.Reflection.Metadata.Ecma335;
using System.Windows.Forms;

namespace VpnMonitor
{
    public class TrayIconProvider
    {
        private readonly VpnService _vpn;
        private readonly ConfigurationOptions _config;
        
        private ConnectedState _currentState;
        private NotifyIcon _icon;
        
        
        public TrayIconProvider(VpnService vpn, ConfigurationOptions config)
        {
            _currentState = ConnectedState.Unknown;
            _vpn = vpn;
            _config = config;
        }
        
        public void CreateTrayIcon()
        {
            _icon = new NotifyIcon
            {
                Icon = new Icon(SystemIcons.WinLogo, 40, 40), 
                Visible = true
            };
        }

        public void CheckVpn()
        {
            var oldState = _currentState;
            var newState = ConnectedState.Unknown;

            if (_vpn.Connected)
            {
                newState = ConnectedState.Connected;
                _icon.Icon = new Icon(SystemIcons.Shield, 40, 40);
                _icon.Text = $"Connected to VPN: {_vpn.VpnNetworkInterface.Description}";
            }
            else
            {                
                newState = ConnectedState.Disconnected;
                _icon.Icon = new Icon(SystemIcons.Error, 40, 40);
                _icon.Text = "Disconnected from the VPN";
            }

            _currentState = newState;

            if (_config.ShowNotification && oldState != newState)
            {
                SendNotification(oldState, newState);
            }
        }
        
        private void SendNotification(ConnectedState oldState, ConnectedState newState)
        {
            if (oldState == ConnectedState.Unknown || newState == ConnectedState.Unknown) return; 

            var notificationText = newState switch
            {
                ConnectedState.Connected => $"Connected to VPN: {_vpn.VpnNetworkInterface.Description}",
                ConnectedState.Disconnected => "Disconnected from the VPN",
                _ => throw new InvalidOperationException()
            };
            
            var notificationTitle = newState switch
            {
                ConnectedState.Connected => $"VPN Connected",
                ConnectedState.Disconnected => "VPN Disconnected",
                _ => throw new InvalidOperationException()
            };

            _icon.ShowBalloonTip(50, notificationTitle, notificationText, ToolTipIcon.Info);
        }
    }
}