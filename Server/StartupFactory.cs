
using System.Diagnostics;
using System.Linq;
using Windows.Devices.WiFi;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace WiFiSurveyor;

public static class StartupFactory
{
	public static void AddWiFiSurveyor(this IServiceCollection services)
	{
		services.AddSingleton(s => s.GetService<ILoggerFactory>().CreateLogger("WiFiSurveyor"));
		services.AddSignalHandlers(Environment.OSVersion.Platform);
		services.AddSingleton<ISignalHub, SignalHub>();
	}

	private static IServiceCollection AddSignalHandlers(this IServiceCollection services, PlatformID OS)
		=> OS switch
		{
			PlatformID.Unix => services.AddLinuxHandlers(),
			PlatformID.Win32NT => services.AddWindowsHandlers(),
			PlatformID.MacOSX => services.AddMacHandlers(),
			_ => throw new NotImplementedException($"{OS} is not currently supported")
		};

	private static IServiceCollection AddPosixHandlers(this IServiceCollection services)
	{
		services.AddSingleton<Func<ProcessStartInfo, Process>>(Process.Start);
		services.AddSingleton<ICommandService, CommandService>();
		services.AddHostedService<SignalService<string>>();
		return services;
	}

	private static IServiceCollection AddLinuxHandlers(this IServiceCollection services)
	{
		services.AddPosixHandlers();
		services.AddSingleton<BrowserLauncher, LinuxBrowserLauncher>();
		services.AddSingleton<ISignalReader<string>, LinuxSignalReader>();
		services.AddSingleton<ISignalParser<string>, LinuxSignalParser>();
		return services;
	}

	private static IServiceCollection AddWindowsHandlers(this IServiceCollection services)
	{
		services.AddSingleton(async _ => (await WiFiAdapter.FindAllAdaptersAsync()).First());
		services.AddSingleton<BrowserLauncher, WindowsBrowserLauncher>();
		services.AddSingleton<ISignalReader<WiFiNetworkReport>, WindowsSignalReader>();
		services.AddSingleton<ISignalParser<WiFiNetworkReport>, WindowsSignalParser>();
		services.AddHostedService<SignalService<WiFiNetworkReport>>();
		return services;
	}

	private static IServiceCollection AddMacHandlers(this IServiceCollection services)
	{
		services.AddPosixHandlers();
		services.AddSingleton<BrowserLauncher, MacBrowserLauncher>();
		services.AddSingleton<ISignalReader<string>, MacSignalReader>();
		services.AddSingleton<ISignalParser<string>, MacSignalParser>();
		return services;
	}
}