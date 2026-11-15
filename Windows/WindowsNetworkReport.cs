using Windows.Devices.WiFi;

namespace WiFiSurveyor.Windows;

public sealed class WindowsNetworkReport(WiFiNetworkReport report) : IWiFiNetworkReport
{
	public IWiFiAvailableNetwork[] AvailableNetworks()
		=>
		[
			.. report.AvailableNetworks
				.Select(GetNetwork)
		];

	private static IWiFiAvailableNetwork GetNetwork(WiFiAvailableNetwork network)
		=> new WindowsAvailableNetwork(network);
}