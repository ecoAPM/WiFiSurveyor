using Windows.Devices.WiFi;
using WiFiSurveyor.Core;

namespace WiFiSurveyor.Windows;

public static class Program
{
	private static void AddWindowsHandlers(IServiceCollection services)
	{
		services.AddSingleton(async _ => await GetWiFiAdapter());
		services.AddSingleton<BrowserLauncher, WindowsBrowserLauncher>();
		services.AddSingleton<ISignalReader<IWiFiNetworkReport>, WindowsSignalReader>();
		services.AddSingleton<ISignalParser<IWiFiNetworkReport>, WindowsSignalParser>();
		services.AddHostedService<SignalService<IWiFiNetworkReport>>();
	}

	private static async Task<IWiFiAdapter> GetWiFiAdapter()
	{
		var adapters = await WiFiAdapter.FindAllAdaptersAsync();
		if (!adapters.Any())
		{
			throw new NullReferenceException("No Wi-Fi adapters found!");
		}

		return new WindowsAdapter(adapters[0]);
	}

	public static async Task Main(string[] args)
		=> await new App(AddWindowsHandlers, args).Run();
}