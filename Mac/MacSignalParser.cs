using CoreWlan;
using Microsoft.Extensions.Logging;
using WiFiSurveyor.Core;

namespace WiFiSurveyor.Mac;

public sealed class MacSignalParser(ILogger logger) : ISignalParser<IReadOnlyList<IWiFiNetwork>>
{
	public IReadOnlyList<Signal> Parse(IReadOnlyList<IWiFiNetwork> networks)
		=> [.. networks.Select(GetSignal).OfType<Signal>().Distinct()];

	private Signal? GetSignal(IWiFiNetwork network)
	{
		try
		{
			var frequency = network.Band switch
			{
				CWChannelBand.TwoGHz => Frequency._2_4_GHz,
				CWChannelBand.FiveGHz => Frequency._5_GHz,
				_ => (Frequency?)null
			};
			if (frequency is null || string.IsNullOrEmpty(network.Bssid))
			{
				return null;
			}

			return new Signal
			{
				SSID = network.Ssid ?? string.Empty,
				MAC = network.Bssid,
				Strength = checked((short)network.Rssi),
				Channel = checked((byte)network.Channel),
				Frequency = frequency.Value
			};
		}
		catch (Exception e)
		{
			logger.LogIf(LogLevel.Warning, "{now}: Could not parse Wi-Fi signal data", DateTime.Now);
			logger.LogIf(LogLevel.Debug, "{exception}", e.ToString());
			return null;
		}
	}
}
