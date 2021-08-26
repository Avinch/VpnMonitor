using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace VpnMonitor
{
    class Program
    {
        private static ServiceProvider _serviceProvider;
        
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();

            SetUpServices(serviceCollection);

            _serviceProvider = serviceCollection.BuildServiceProvider();

            var startupService = _serviceProvider.GetService<Startup>();
            
            startupService?.Start();
        }

        private static void SetUpServices(IServiceCollection services)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            
            var configOptions = new ConfigurationOptions();
            config.Bind(configOptions);

            services.AddTransient<Startup, Startup>();

            services.AddSingleton(configOptions);
            services.AddSingleton<TrayIconProvider, TrayIconProvider>();
            services.AddSingleton<VpnService, VpnService>();
        }
    }
}