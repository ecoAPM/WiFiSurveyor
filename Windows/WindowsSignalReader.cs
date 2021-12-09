using WiFiSurveyor.Core;
using Windows.Devices.WiFi;

namespace WiFiSurveyor.Windows;

public sealed class WindowsSignalReader : ISignalReader<WiFiNetworkReport>
{
	private WiFiAdapter _adapter = null!;
	private readonly Task<WiFiAdapter> _newAdapter;

	public WindowsSignalReader(Task<WiFiAdapter> adapterFactory)
		=> _newAdapter = adapterFactory;

	public async Task<WiFiNetworkReport> Read()
	{
		_adapter ??= await _newAdapter;
		await _adapter.ScanAsync();
		return _adapter.NetworkReport;
	}
}