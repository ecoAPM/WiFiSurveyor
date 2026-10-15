namespace WiFiSurveyor.Mac;

public sealed record WiFiNetwork(string? Ssid, string? Bssid, long Rssi, long Channel, long Band);
