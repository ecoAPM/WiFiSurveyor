using WiFiSurveyor.Core;

namespace WiFiSurveyor.Linux;

public static class Program
{
	public static void AddLinuxHandlers(this IServiceCollection services)
	{
		services.AddPosixHandlers();
		services.AddSingleton<IBrowserLauncher, LinuxBrowserLauncher>();
		services.AddSingleton<ISignalReader<string>, LegacyLinuxSignalReader>();
		services.AddSingleton<ISignalParser<string>, LegacyLinuxSignalParser>();
	}

	public static async Task Main(string[] args)
		=> await new App(AddLinuxHandlers, args).Run();
}