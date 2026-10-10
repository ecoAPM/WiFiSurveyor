using CoreWlan;

namespace WiFiSurveyor.Mac;

public interface IWiFiNetwork
{
	string? Ssid { get; }
	string? Bssid { get; }
	nint Rssi { get; }
	nint Channel { get; }
	CWChannelBand Band { get; }
}
