using System.Text.RegularExpressions;

namespace WiFiSurveyor.Linux;

public static partial class Patterns
{
	[GeneratedRegex(@"^(.+)\(on .*\)")]
	public static partial Regex Address();

	[GeneratedRegex("SSID: (.+)")]
	public static partial Regex SSID();

	[GeneratedRegex(@"freq: (\d+)")]
	public static partial Regex Frequency();

	[GeneratedRegex(@"\* primary channel: (\d+)")]
	public static partial Regex Channel();

	[GeneratedRegex(@"DS Parameter set: channel (\d+)")]
	public static partial Regex BackupChannel();

	[GeneratedRegex(@"signal: (-\d+)")]
	public static partial Regex Signal();
}