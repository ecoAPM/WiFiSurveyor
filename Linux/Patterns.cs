using System.Text.RegularExpressions;

namespace WiFiSurveyor.Linux;

public static partial class Patterns
{
	[GeneratedRegex("Address: (.+)")]
	public static partial Regex Address();

	[GeneratedRegex("SSID:\"(.+)\"")]
	public static partial Regex SSID();

	[GeneratedRegex("Frequency:(\\d)")]
	public static partial Regex Frequency();

	[GeneratedRegex("Channel:(\\d+)")]
	public static partial Regex Channel();

	[GeneratedRegex("Signal level=(-\\d+)")]
	public static partial Regex Signal();
}