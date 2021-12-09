using WiFiSurveyor.Core;

namespace WiFiSurveyor.Windows;

public sealed class WindowsSignalParser : ISignalParser<IWiFiNetworkReport>
{
	public IReadOnlyList<Signal> Parse(IWiFiNetworkReport results)
		=> results.AvailableNetworks()
			.Select(GetSignal)
			.ToArray();

	private static Signal GetSignal(IWiFiAvailableNetwork r)
		=> new()
		{
			MAC = r.Bssid,
			SSID = r.Ssid,
			Frequency = r.ChannelCenterFrequencyInKilohertz / 1_000_000 == 5 ? Frequency._5_GHz : Frequency._2_4_GHz,
			Strength = Convert.ToInt16(r.NetworkRssiInDecibelMilliwatts)
		};
}