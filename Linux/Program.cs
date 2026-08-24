using WiFiSurveyor.Core;

namespace WiFiSurveyor.Linux;

public static class Program
{
	public static void AddLinuxHandlers(this IServiceCollection services)
	{
		services.AddPosixHandlers();
		services.AddSingleton<IBrowserLauncher, LinuxBrowserLauncher>();
		services.AddSingleton<IDeviceLocator, LegacyLinuxDeviceLocator>();
		services.AddSingleton<ISignalParser<string>, LegacyLinuxSignalParser>();
		services.AddSingleton<ISignalReader<string>, LegacyLinuxSignalReader>();
	}

	public static async Task Main(string[] args)
		=> await new App(AddLinuxHandlers, args).Run();
}