using WiFiSurveyor.Core;

namespace WiFiSurveyor.Windows;

public sealed class WindowsSignalParser : ISignalParser<IWiFiNetworkReport>
{
	private readonly ILogger _logger;

	public WindowsSignalParser(ILogger logger)
		=> _logger = logger;

	public IReadOnlyList<Signal> Parse(IWiFiNetworkReport results)
		=> results.AvailableNetworks()
			.Select(GetSignal)
			.Where(s => s is not null)
			.Cast<Signal>()
			.ToArray();

	private Signal? GetSignal(IWiFiAvailableNetwork result)
	{
		try
		{
			return new Signal
			{
				MAC = result.Bssid,
				SSID = result.Ssid,
				Frequency = GetFrequency(result.ChannelCenterFrequencyInKilohertz),
				Channel = GetChannel(result.ChannelCenterFrequencyInKilohertz),
				Strength = Convert.ToInt16(result.NetworkRssiInDecibelMilliwatts)
			};
		}
		catch (Exception e)
		{
			_logger.LogIf(LogLevel.Warning, "{now}: Could not parse signal data -- {result}", DateTime.Now, result);
			_logger.LogIf(LogLevel.Debug, "{exception}", e.ToString());
			return null;
		}
	}

	private static byte GetChannel(int kHz)
	{
		var baseFreq = GetFrequency(kHz) == Frequency._5_GHz ? 5_000_000 : 2_407_000;
		var channel = (kHz - baseFreq) / 5_000;
		return Convert.ToByte(channel);
	}

	private static Frequency GetFrequency(int kHz)
		=> kHz / 1_000_000 == 5
			? Frequency._5_GHz
			: Frequency._2_4_GHz;
}