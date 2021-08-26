# VPN Monitor

A little tray-icon to indicate whether you're actively connected to a named VPN.

Built with WinForms / .NET 5

### Configuration

Update `appsettings.json` with the following:

- `VpnName`: Name of the VPN that you wish to check the connection status of
- `PollingIntervalMs`: How frequently the connection state should be checked in milliseconds (default: `500`)
- `ShowNotification`: Whether you wish to recieve a notification if the connection state has been changed (default: `true`)

