using WiFiSurveyor.Core;

namespace WiFiSurveyor.Linux;

public static class Program
{
	private static void AddLinuxHandlers(IServiceCollection services)
	{
		services.AddPosixHandlers();
		services.AddSingleton<BrowserLauncher, LinuxBrowserLauncher>();
		services.AddSingleton<ISignalReader<string>, LinuxSignalReader>();
		services.AddSingleton<ISignalParser<string>, LinuxSignalParser>();
	}

	public static async Task Main(string[] args)
		=> await new App(AddLinuxHandlers, args).Run();
}