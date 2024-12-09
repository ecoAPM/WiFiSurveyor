using WiFiSurveyor.Core;

namespace WiFiSurveyor.Linux;

public sealed class LinuxSignalParser : ISignalParser<string>
{
	private readonly ILogger _logger;

	public LinuxSignalParser(ILogger logger)
		=> _logger = logger;

	public IReadOnlyList<Signal> Parse(string results)
		=> results
			.Split(" Cell ")
			.Skip(1)
			.Select(GetSignal)
			.Where(s => s is not null)
			.Cast<Signal>()
			.ToArray();

	private Signal? GetSignal(string result)
	{
		try
		{
			var mac = Patterns.Address().Match(result).Groups[1].Value;
			var ssid = Patterns.SSID().Match(result).Groups[1].Value;
			var freq = Patterns.Frequency().Match(result).Groups[1].Value;
			var channel = Patterns.Channel().Match(result).Groups[1].Value;
			var dbm = Patterns.Signal().Match(result).Groups[1].Value;

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
			_logger.LogIf(LogLevel.Warning, "{now}: Could not parse signal data -- {result}", DateTime.Now, result);
			_logger.LogIf(LogLevel.Debug, "{exception}", e.ToString());
			return null;
		}
	}
}