using WiFiSurveyor.Core;

namespace WiFiSurveyor.Mac;

public sealed class MacSignalParser : ISignalParser<string>
{
	public IReadOnlyList<Signal> Parse(string results)
		=> results.Split("\n", StringSplitOptions.RemoveEmptyEntries)
			.Skip(1)
			.Select(GetSignal)
			.ToArray();

	private static Signal GetSignal(string line)
		=> new()
		{
			SSID = line[..32].Trim(),
			MAC = line[32..50].Trim(),
			Strength = short.Parse(line[50..55].Trim()),
			Channel = GetChannel(line[55..64].Trim()),
			Frequency = GetFrequency(line[55..64].Trim())
		};

	private static Frequency GetFrequency(string column)
		=> GetChannel(column) < 32
			? Frequency._2_4_GHz
			: Frequency._5_GHz;

	private static byte GetChannel(string column)
		=> byte.Parse(column.Split(',')[0]);
}