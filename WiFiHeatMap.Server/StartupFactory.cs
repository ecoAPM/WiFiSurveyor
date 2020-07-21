using System;
using Microsoft.Extensions.DependencyInjection;

namespace WiFiHeatMap.Server
{
    public static class StartupFactory
    {
        public static void AddWiFiHeatMap(this IServiceCollection services)
        {
            services.AddSignalHandlers(Environment.OSVersion.Platform);
            services.AddSingleton<ISignalHub, SignalHub>();
            services.AddHostedService<SignalService>();
        }

        private static void AddSignalHandlers(this IServiceCollection services, PlatformID OS)
        {
            switch (OS)
            {
                case PlatformID.Unix:
                    services.AddSingleton<ICommandService, LinuxCommandService>();
                    services.AddSingleton<ISignalReader, LinuxSignalReader>();
                    services.AddSingleton<ISignalParser, LinuxSignalParser>();
                    break;

                case PlatformID.Win32NT:
                    services.AddSingleton<ISignalReader, WindowsSignalReader>();
                    services.AddSingleton<ISignalParser, WindowsSignalParser>();
                    break;

                case PlatformID.MacOSX:
                    services.AddSingleton<ISignalReader, MacSignalReader>();
                    services.AddSingleton<ISignalParser, MacSignalParser>();
                    break;

                default:
                    throw new NotImplementedException($"{OS} is not currently supported");
            }
        }
    }
}