using System;
using System.Diagnostics;
using System.Drawing;
using System.Reflection.Metadata.Ecma335;
using System.Windows.Forms;

namespace VpnMonitor
{
    public class TrayIconProvider
    {
        private readonly ConfigurationOptions _config;
        
        private NotifyIcon _icon;
        
        public TrayIconProvider(ConfigurationOptions config)
        {
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

        public void SetIconConnectionState(bool connected, string vpnName = "Unknown")
        {
            if (connected)
            {
                _icon.Icon = new Icon(SystemIcons.Shield, 40, 40);
                _icon.Text = $"Connected to VPN: {vpnName}";
            }
            else
            {
                _icon.Icon = new Icon(SystemIcons.Error, 40, 40);
                _icon.Text = "Disconnected from the VPN";
            }
        }
        
        public void SendConnectionStateChangeNotification(bool connected, string vpnName = "Unknown")
        {
            if (!_config.ShowConnectedNotification) return;
            
            var notificationText = connected switch
            {
                true => $"Connection established to VPN: {vpnName}",
                false => "Disconnected from the VPN"
            };
            
            var notificationTitle = connected switch
            {
                true => "VPN Connected",
                false => "VPN Disconnected",
            };

            _icon.ShowBalloonTip(50, notificationTitle, notificationText, ToolTipIcon.Info);
        }
        
        public void SendConnectedReminderNotification()
        {
            if (!_config.ShowReminderNotification) return;
            
            _icon.ShowBalloonTip(50, "Reminder", 
                $"You are still connected to the VPN", ToolTipIcon.Info);
        }
    }
}