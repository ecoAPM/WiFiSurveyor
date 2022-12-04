using WiFiSurveyor.Core;

namespace WiFiSurveyor.Mac;

public sealed class MacSignalParser : ISignalParser<string>
{
	private readonly ILogger _logger;

	public MacSignalParser(ILogger logger)
		=> _logger = logger;

	public IReadOnlyList<Signal> Parse(string results)
		=> results.Split("\n", StringSplitOptions.RemoveEmptyEntries)
			.Skip(1)
			.Select(GetSignal)
			.Where(s => s is not null)
			.Cast<Signal>()
			.ToArray();

	private Signal? GetSignal(string line)
	{
		try
		{
			return new()
			{
				SSID = line[..32].Trim(),
				MAC = line[32..50].Trim(),
				Strength = short.Parse(line[50..55].Trim()),
				Channel = GetChannel(line[55..64].Trim()),
				Frequency = GetFrequency(line[55..64].Trim())
			};
		}
		catch (Exception)
		{
			_logger.LogIf(LogLevel.Warning, "Could not parse signal data: {0}", line);
			return null;
		}
	}

	private static Frequency GetFrequency(string column)
		=> GetChannel(column) < 32
			? Frequency._2_4_GHz
			: Frequency._5_GHz;

	private static byte GetChannel(string column)
		=> byte.Parse(column.Split(',')[0]);
}