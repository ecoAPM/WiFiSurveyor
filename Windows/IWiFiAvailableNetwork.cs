namespace WiFiSurveyor.Windows;

public interface IWiFiAvailableNetwork
{
	string Bssid { get; }
	string Ssid { get; }
	int ChannelCenterFrequencyInKilohertz { get; }
	double NetworkRssiInDecibelMilliwatts { get; }
}