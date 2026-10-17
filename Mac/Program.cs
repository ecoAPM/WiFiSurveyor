using WiFiSurveyor.Core;

namespace WiFiSurveyor.Mac;

public static class Program
{
	public static void AddMacHandlers(this IServiceCollection services)
	{
		services.AddHostedService<SignalService<IReadOnlyList<WiFiNetwork>>>();
		services.AddSingleton<Func<IReadOnlyList<WiFiNetwork>?>>(_ => CoreWlan.Scan);
		services.AddSingleton<IBrowserLauncher, MacBrowserLauncher>();
		services.AddSingleton<ISignalReader<IReadOnlyList<WiFiNetwork>>, MacSignalReader>();
		services.AddSingleton<ISignalParser<IReadOnlyList<WiFiNetwork>>, MacSignalParser>();
	}

	public static async Task Main(string[] args)
		=> await new App(AddMacHandlers, args).Run();
}