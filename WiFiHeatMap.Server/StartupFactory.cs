using System;
using System.Linq;
using Windows.Devices.WiFi;
using Microsoft.Extensions.DependencyInjection;

namespace WiFiHeatMap.Server
{
    public static class StartupFactory
    {
        public static void AddWiFiHeatMap(this IServiceCollection services)
        {
            services.AddSignalHandlers(Environment.OSVersion.Platform);
            services.AddSingleton<ISignalHub, SignalHub>();
        }

        private static void AddSignalHandlers(this IServiceCollection services, PlatformID OS)
        {
            switch (OS)
            {
                case PlatformID.Unix:
                    services.AddSingleton<ICommandService, LinuxCommandService>();
                    services.AddSingleton<ISignalReader<string>, LinuxSignalReader>();
                    services.AddSingleton<ISignalParser<string>, LinuxSignalParser>();
                    services.AddHostedService<SignalService<string>>();
                    break;

                case PlatformID.Win32NT:
                    services.AddSingleton(async s => (await WiFiAdapter.FindAllAdaptersAsync()).First());
                    services.AddSingleton<ISignalReader<WiFiNetworkReport>, WindowsSignalReader>();
                    services.AddSingleton<ISignalParser<WiFiNetworkReport>, WindowsSignalParser>();
                    services.AddHostedService<SignalService<WiFiNetworkReport>>();
                    break;

                case PlatformID.MacOSX:
                    services.AddSingleton<ISignalReader<string>, MacSignalReader>();
                    services.AddSingleton<ISignalParser<string>, MacSignalParser>();
                    services.AddHostedService<SignalService<string>>();
                    break;

                default:
                    throw new NotImplementedException($"{OS} is not currently supported");
            }
        }
    }
}