using Windows.Devices.WiFi;

namespace WiFiSurveyor.Windows;

public sealed class WindowsAvailableNetwork : IWiFiAvailableNetwork
{
	private readonly WiFiAvailableNetwork _network;

	public WindowsAvailableNetwork(WiFiAvailableNetwork network)
		=> _network = network;

	public string Bssid
		=> _network.Bssid;

	public string Ssid
		=> _network.Ssid;

	public int ChannelCenterFrequencyInKilohertz
		=> _network.ChannelCenterFrequencyInKilohertz;

	public double NetworkRssiInDecibelMilliwatts
		=> _network.NetworkRssiInDecibelMilliwatts;
}