using System.Diagnostics;

namespace WiFiSurveyor.Core;

public static class StartupFactory
{
	public static void AddWiFiSurveyor(this IServiceCollection services)
	{
		services.AddSingleton(s => s.GetService<ILoggerFactory>()!.CreateLogger("WiFiSurveyor"));
		services.AddSingleton<ISignalHub, SignalHub>();
	}

	public static IServiceCollection AddPosixHandlers(this IServiceCollection services)
	{
		services.AddSingleton<Func<ProcessStartInfo, Process?>>(Process.Start);
		services.AddSingleton<ICommandService, CommandService>();
		services.AddHostedService<SignalService<string>>();
		return services;
	}
}