using WiFiSurveyor.Core;

namespace WiFiSurveyor.Windows;

public static class Program
{
	public static void AddWindowsHandlers(this IServiceCollection services)
	{
		services.AddSingleton<Func<Task<IWiFiAdapter>>>(WindowsAdapter.Default);
		services.AddSingleton<IBrowserLauncher, WindowsBrowserLauncher>();
		services.AddSingleton<ISignalReader<IWiFiNetworkReport>, WindowsSignalReader>();
		services.AddSingleton<ISignalParser<IWiFiNetworkReport>, WindowsSignalParser>();
		services.AddHostedService<SignalService<IWiFiNetworkReport>>();
	}

	public static async Task Main(string[] args)
		=> await new App(AddWindowsHandlers, args).Run();
}