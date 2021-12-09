using WiFiSurveyor.Core;

namespace WiFiSurveyor.Mac;

public static class Program
{
	public static void AddMacHandlers(this IServiceCollection services)
	{
		services.AddPosixHandlers();
		services.AddSingleton<IBrowserLauncher, MacBrowserLauncher>();
		services.AddSingleton<ISignalReader<string>, MacSignalReader>();
		services.AddSingleton<ISignalParser<string>, MacSignalParser>();
	}

	public static async Task Main(string[] args)
		=> await new App(AddMacHandlers, args).Run();
}