using Windows.Devices.WiFi;

namespace WiFiSurveyor.Windows;

public class WindowsAdapter : IWiFiAdapter
{
	private readonly WiFiAdapter _adapter;

	public WindowsAdapter(WiFiAdapter adapter)
		=> _adapter = adapter;

	public async Task ScanAsync()
		=> await _adapter.ScanAsync();

	public IWiFiNetworkReport NetworkReport
		=> new WindowsNetworkReport(_adapter.NetworkReport);
}