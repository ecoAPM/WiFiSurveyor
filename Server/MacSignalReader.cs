using System.Diagnostics;

namespace WiFiSurveyor;

public sealed class MacSignalReader : PosixSignalReader
{
	protected override ProcessStartInfo Info => new ProcessStartInfo("/System/Library/PrivateFrameworks/Apple80211.framework/Versions/Current/Resources/airport", " -s");

	public MacSignalReader(ICommandService commandService) : base(commandService)
	{
	}
}