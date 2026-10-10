using CoreWlan;

namespace WiFiSurveyor.Mac;

public sealed class WiFiNetwork(CWNetwork network) : IWiFiNetwork
{
	public string? Ssid { get; } = network.Ssid;
	public string? Bssid { get; } = network.Bssid;
	public nint Rssi { get; } = network.RssiValue;
	public nint Channel { get; } = network.WlanChannel?.ChannelNumber ?? 0;
	public CWChannelBand Band { get; } = network.WlanChannel?.ChannelBand ?? CWChannelBand.Unknown;
}
