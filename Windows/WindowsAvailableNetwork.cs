using Windows.Devices.WiFi;

namespace WiFiSurveyor.Windows;

public sealed class WindowsAvailableNetwork(WiFiAvailableNetwork network) : IWiFiAvailableNetwork
{
	public string Bssid
		=> network.Bssid;

	public string Ssid
		=> network.Ssid;

	public int ChannelCenterFrequencyInKilohertz
		=> network.ChannelCenterFrequencyInKilohertz;

	public double NetworkRssiInDecibelMilliwatts
		=> network.NetworkRssiInDecibelMilliwatts;
}