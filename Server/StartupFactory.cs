using System;
using System.Diagnostics;
using System.Linq;
using Windows.Devices.WiFi;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace WiFiSurveyor
{
    public static class StartupFactory
    {
        public static void AddWiFiSurveyor(this IServiceCollection services)
        {
            services.AddSingleton(s => s.GetService<ILoggerFactory>().CreateLogger("WiFiSurveyor"));
            services.AddSignalHandlers(Environment.OSVersion.Platform);
            services.AddSingleton<ISignalHub, SignalHub>();
        }

        private static void AddSignalHandlers(this IServiceCollection services, PlatformID OS)
        {
            switch (OS)
            {
                case PlatformID.Unix:
                    services.AddLinuxHandlers();
                    break;

                case PlatformID.Win32NT:
                    services.AddWindowsHandlers();
                    break;

                case PlatformID.MacOSX:
                    services.AddMacHandlers();
                    break;

                default:
                    throw new NotImplementedException($"{OS} is not currently supported");
            }
        }

        private static void AddLinuxHandlers(this IServiceCollection services)
        {
            services.AddSingleton<Func<ProcessStartInfo,Process>>(Process.Start);
            services.AddSingleton<ICommandService, LinuxCommandService>();
            services.AddSingleton<ISignalReader<string>, LinuxSignalReader>();
            services.AddSingleton<ISignalParser<string>, LinuxSignalParser>();
            services.AddHostedService<SignalService<string>>();
        }

        private static void AddWindowsHandlers(this IServiceCollection services)
        {
            services.AddSingleton(async s => (await WiFiAdapter.FindAllAdaptersAsync()).First());
            services.AddSingleton<ISignalReader<WiFiNetworkReport>, WindowsSignalReader>();
            services.AddSingleton<ISignalParser<WiFiNetworkReport>, WindowsSignalParser>();
            services.AddHostedService<SignalService<WiFiNetworkReport>>();
        }

        private static void AddMacHandlers(this IServiceCollection services)
        {
            services.AddSingleton<ISignalReader<string>, MacSignalReader>();
            services.AddSingleton<ISignalParser<string>, MacSignalParser>();
            services.AddHostedService<SignalService<string>>();
        }
    }
}