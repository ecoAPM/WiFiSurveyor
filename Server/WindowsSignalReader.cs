using System;
using System.Threading.Tasks;
using Windows.Devices.WiFi;

namespace WiFiSurveyor;

public class WindowsSignalReader : ISignalReader<WiFiNetworkReport>
{
	private WiFiAdapter _adapter;
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
