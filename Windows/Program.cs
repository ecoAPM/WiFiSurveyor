using WiFiSurveyor.Core;
using Windows.Devices.WiFi;

namespace WiFiSurveyor.Windows;

public static class Program
{
	private static void AddWindowsHandlers(IServiceCollection services)
	{
		services.AddSingleton(async _ => (await WiFiAdapter.FindAllAdaptersAsync()).First());
		services.AddSingleton<BrowserLauncher, WindowsBrowserLauncher>();
		services.AddSingleton<ISignalReader<WiFiNetworkReport>, WindowsSignalReader>();
		services.AddSingleton<ISignalParser<WiFiNetworkReport>, WindowsSignalParser>();
		services.AddHostedService<SignalService<WiFiNetworkReport>>();
	}
	public static async Task Main(string[] args)
		=> await new App(AddWindowsHandlers, args).Run();
}