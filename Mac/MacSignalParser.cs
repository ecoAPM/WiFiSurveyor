using WiFiSurveyor.Core;

namespace WiFiSurveyor.Mac;

public sealed class MacSignalParser : ISignalParser<string>
{
	public IList<Signal> Parse(string results)
			=> results.Split("\n", StringSplitOptions.RemoveEmptyEntries)
				.Skip(1)
				.Select(GetSignal)
				.ToList();

	private static Signal GetSignal(string line)
		=> new Signal
		{
			SSID = line[..32].Trim(),
			MAC = line[32..50].Trim(),
			Strength = short.Parse(line[50..55].Trim()),
			Frequency = GetFrequency(line[55..64].Trim())
		};

	private static Frequency GetFrequency(string channel)
		=> int.Parse(channel.Split(',')[0]) < 32
			? Frequency._2_4_GHz
			: Frequency._5_GHz;
}