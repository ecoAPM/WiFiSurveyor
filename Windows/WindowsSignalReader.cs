using WiFiSurveyor.Core;

namespace WiFiSurveyor.Windows;

public sealed class WindowsSignalReader : ISignalReader<IWiFiNetworkReport>
{
	private readonly Func<Task<IWiFiAdapter>> _newAdapter;
	private IWiFiAdapter? _adapter;

	public WindowsSignalReader(Func<Task<IWiFiAdapter>> adapterFactory)
		=> _newAdapter = adapterFactory;

	public async Task<IWiFiNetworkReport> Read()
	{
		_adapter ??= await _newAdapter();
		await _adapter.ScanAsync();
		return _adapter.NetworkReport;
	}
}