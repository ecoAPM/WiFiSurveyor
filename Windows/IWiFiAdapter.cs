namespace WiFiSurveyor.Windows;

public interface IWiFiAdapter
{
	IWiFiNetworkReport NetworkReport { get; }
	Task ScanAsync();
}