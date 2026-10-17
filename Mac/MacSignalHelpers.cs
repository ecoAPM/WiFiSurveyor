using System.Text.Json;

namespace WiFiSurveyor.Mac;

public static class MacSignalHelpers
{
	public const string ConnectedNetworkKey = "spairport_airport_other_local_wireless_networks";
	public const string DisconnectedNetworkKey = "spairport_airport_local_wireless_networks";

	public static JsonElement GetSignals(this JsonElement json)
		=> json.TryGetProperty(ConnectedNetworkKey, out var networksConnected)
			? networksConnected
			: json.GetProperty(DisconnectedNetworkKey);
}