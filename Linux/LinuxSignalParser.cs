using WiFiSurveyor.Core;

namespace WiFiSurveyor.Linux;

public sealed class LinuxSignalParser : ISignalParser<string>
{
	public IReadOnlyList<Signal> Parse(string results)
		=> results
			.Split(" Cell ")
			.Skip(1)
			.Select(GetSignal)
			.ToArray();

	private static Signal GetSignal(string result)
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
}