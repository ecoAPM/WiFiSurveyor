using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace WiFiSurveyor;

public sealed class LinuxSignalParser : ISignalParser<string>
{
	public IList<Signal> Parse(string results)
	{
		var accessPoints = results.Split(" Cell ").Skip(1);

		var signals = new List<Signal>();
		foreach (var result in accessPoints)
		{
			var mac = Regex.Match(result, "Address: (.+)").Groups[1].Value;
			var ssid = Regex.Match(result, "SSID:\"(.+)\"").Groups[1].Value;
			var freq = Regex.Match(result, "Frequency:(\\d)").Groups[1].Value;
			var dbm = Regex.Match(result, "Signal level=(-\\d+)").Groups[1].Value;

			var signal = new Signal
			{
				MAC = mac,
				SSID = ssid.Replace(@"\x00", ""),
				Frequency = (freq == "2" ? Frequency._2_4_GHz : Frequency._5_GHz),
				Strength = short.Parse(dbm)
			};

			signals.Add(signal);
		}
		return signals;
	}
}