using WiFiSurveyor.Core;

namespace WiFiSurveyor.Linux;

public sealed class LegacyLinuxSignalParser(ILogger logger) : ISignalParser<string>
{
	public IReadOnlyList<Signal> Parse(string results)
		=>
		[
			.. results
				.Split(" Cell ")
				.Skip(1)
				.Select(GetSignal)
				.Where(s => s is not null)
				.Cast<Signal>()
		];

	private Signal? GetSignal(string result)
	{
		try
		{
			var mac = LegacyPatterns.Address().Match(result).Groups[1].Value;
			var ssid = LegacyPatterns.SSID().Match(result).Groups[1].Value;
			var freq = LegacyPatterns.Frequency().Match(result).Groups[1].Value;
			var channel = LegacyPatterns.Channel().Match(result).Groups[1].Value;
			var dbm = LegacyPatterns.Signal().Match(result).Groups[1].Value;

			return new Signal
			{
				MAC = mac,
				SSID = ssid.Replace(@"\x00", ""),
				Frequency = freq == "2" ? Frequency._2_4_GHz : Frequency._5_GHz,
				Channel = byte.Parse(channel),
				Strength = short.Parse(dbm)
			};
		}
		catch (Exception e)
		{
			logger.LogIf(LogLevel.Warning, "{now}: Could not parse signal data -- {result}", DateTime.Now, result);
			logger.LogIf(LogLevel.Debug, "{exception}", e.ToString());
			return null;
		}
	}
}