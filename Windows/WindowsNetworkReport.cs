using Windows.Devices.WiFi;

namespace WiFiSurveyor.Windows;

public sealed class WindowsNetworkReport : IWiFiNetworkReport
{
	private readonly WiFiNetworkReport _report;

	public WindowsNetworkReport(WiFiNetworkReport report)
		=> _report = report;

	public IReadOnlyList<IWiFiAvailableNetwork> AvailableNetworks()
		=> _report.AvailableNetworks
			.Select(GetNetwork)
			.ToArray();

	private static IWiFiAvailableNetwork GetNetwork(WiFiAvailableNetwork network)
		=> new WindowsAvailableNetwork(network);
}