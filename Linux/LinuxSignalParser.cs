using System.Text.RegularExpressions;
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
		var mac = Regex.Match(result, "Address: (.+)").Groups[1].Value;
		var ssid = Regex.Match(result, "SSID:\"(.+)\"").Groups[1].Value;
		var freq = Regex.Match(result, "Frequency:(\\d)").Groups[1].Value;
		var dbm = Regex.Match(result, "Signal level=(-\\d+)").Groups[1].Value;

		return new Signal
		{
			MAC = mac,
			SSID = ssid.Replace(@"\x00", ""),
			Frequency = freq == "2" ? Frequency._2_4_GHz : Frequency._5_GHz,
			Strength = short.Parse(dbm)
		};
	}
}