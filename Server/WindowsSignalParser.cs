using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Devices.WiFi;

namespace WiFiSurveyor;

public sealed class WindowsSignalParser : ISignalParser<WiFiNetworkReport>
{
	public IList<Signal> Parse(WiFiNetworkReport results)
	{
		return results.AvailableNetworks
			.Select(r => new Signal
			{
				MAC = r.Bssid,
				SSID = r.Ssid,
				Frequency = r.ChannelCenterFrequencyInKilohertz / 1_000_000 == 5 ? Frequency._5_GHz : Frequency._2_4_GHz,
				Strength = Convert.ToInt16(r.NetworkRssiInDecibelMilliwatts)
			})
			.ToList();
	}
}