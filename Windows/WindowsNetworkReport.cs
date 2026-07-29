using Windows.Devices.WiFi;

namespace WiFiSurveyor.Windows;

public sealed class WindowsNetworkReport(WiFiNetworkReport report) : IWiFiNetworkReport
{
	public IReadOnlyList<IWiFiAvailableNetwork> AvailableNetworks()
		=> report.AvailableNetworks
			.Select(GetNetwork)
			.ToArray();

	private static IWiFiAvailableNetwork GetNetwork(WiFiAvailableNetwork network)
		=> new WindowsAvailableNetwork(network);
}