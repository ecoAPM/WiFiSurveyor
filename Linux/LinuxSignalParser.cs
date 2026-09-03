using WiFiSurveyor.Core;

namespace WiFiSurveyor.Linux;

public sealed class LinuxSignalParser(ILogger logger) : ISignalParser<string>
{
	public IReadOnlyList<Signal> Parse(string results)
		=>
		[
			.. results
				.Split($"{Environment.NewLine}BSS ")
				.Select(GetSignal)
				.Where(s => s is not null)
				.Cast<Signal>()
		];

	private Signal? GetSignal(string result)
	{
		try
		{
			var mac = Patterns.Address().Match(result).Groups[1].Value;
			var ssid = Patterns.SSID().Match(result).Groups[1].Value;
			var freq = Patterns.Frequency().Match(result).Groups[1].Value;
			var channel = Patterns.Channel().IsMatch(result) ? Patterns.Channel().Match(result).Groups[1].Value
				: Patterns.BackupChannel().IsMatch(result) ? Patterns.BackupChannel().Match(result).Groups[1].Value
				: CalculateChannelFromFrequency(freq).ToString();
			var dbm = Patterns.Signal().Match(result).Groups[1].Value;

			return new Signal
			{
				MAC = mac,
				SSID = ssid.Replace(@"\x00", ""),
				Frequency = freq.StartsWith('2') ? Frequency._2_4_GHz : Frequency._5_GHz,
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

	private static int CalculateChannelFromFrequency(string frequency)
	{
		if (!ushort.TryParse(frequency, out var freq))
			throw new ArgumentException($"Invalid frequency {frequency}", nameof(frequency));

		return (freq / 1_000) switch
		{
			5 => (freq - 5000) / 5,
			_ => freq == 2484 ? 14 : (freq - 2412) / 5
		};
	}
}