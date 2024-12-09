using System.Text;
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
			.Select(line => GetSignal(Encoding.UTF8.GetBytes(line)))
			.Where(s => s is not null)
			.Cast<Signal>()
			.ToArray();

	private Signal? GetSignal(ReadOnlySpan<byte> line)
	{
		try
		{
			return new()
			{
				SSID = Encoding.UTF8.GetString(line[..32]).Trim(),
				MAC = Encoding.UTF8.GetString(line[32..50]).Trim(),
				Strength = short.Parse(Encoding.UTF8.GetString(line[50..55]).Trim()),
				Channel = GetChannel(Encoding.UTF8.GetString(line[55..64]).Trim()),
				Frequency = GetFrequency(Encoding.UTF8.GetString(line[55..64]).Trim())
			};
		}
		catch (Exception e)
		{
			_logger.LogIf(LogLevel.Warning, "{now}: Could not parse signal data -- {data}", DateTime.Now, Encoding.UTF8.GetString(line));
			_logger.LogIf(LogLevel.Debug, "{exception}", e.ToString());
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