using Windows.Devices.WiFi;

namespace WiFiSurveyor.Windows;

public sealed class WindowsAdapter : IWiFiAdapter
{
	private readonly WiFiAdapter _adapter;

	public WindowsAdapter(WiFiAdapter adapter)
		=> _adapter = adapter;

	public async Task ScanAsync()
		=> await _adapter.ScanAsync();

	public IWiFiNetworkReport NetworkReport
		=> new WindowsNetworkReport(_adapter.NetworkReport);

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