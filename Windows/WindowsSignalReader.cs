using WiFiSurveyor.Core;

namespace WiFiSurveyor.Windows;

public sealed class WindowsSignalReader(Func<Task<IWiFiAdapter>> adapterFactory) : ISignalReader<IWiFiNetworkReport>
{
	private IWiFiAdapter? _adapter;

	public async Task<IWiFiNetworkReport> Read()
	{
		_adapter ??= await adapterFactory();
		await _adapter.ScanAsync();
		return _adapter.NetworkReport;
	}
}