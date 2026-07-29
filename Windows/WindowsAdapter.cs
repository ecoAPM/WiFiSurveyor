using Windows.Devices.WiFi;

namespace WiFiSurveyor.Windows;

public sealed class WindowsAdapter(WiFiAdapter adapter) : IWiFiAdapter
{
	public async Task ScanAsync()
		=> await adapter.ScanAsync();

	public IWiFiNetworkReport NetworkReport
		=> new WindowsNetworkReport(adapter.NetworkReport);

	public static async Task<IWiFiAdapter> Default()
	{
		var adapters = await WiFiAdapter.FindAllAdaptersAsync();
		if (!adapters.Any())
		{
			throw new KeyNotFoundException("No Wi-Fi adapters found");
		}

		return new WindowsAdapter(adapters[0]);
	}
}