using System.Linq;
using System.Net.NetworkInformation;
using Microsoft.Extensions.Configuration;

namespace VpnMonitor
{
    public class VpnService
    {
        private bool _vpnLoaded;
        private readonly ConfigurationOptions _config;
        public bool Connected => CheckConnection() && VpnNetworkInterface.OperationalStatus == OperationalStatus.Up;

        public NetworkInterface VpnNetworkInterface { get; private set; }
        
        public VpnService(ConfigurationOptions config)
        {
            _config = config;
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
    }
}