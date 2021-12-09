namespace WiFiSurveyor.Windows;

public interface IWiFiNetworkReport
{
	IReadOnlyList<IWiFiAvailableNetwork> AvailableNetworks();
}