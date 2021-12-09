using System.Diagnostics;
using WiFiSurveyor.Core;

namespace WiFiSurveyor.Mac;

public sealed class MacSignalReader : PosixSignalReader
{
	public MacSignalReader(ICommandService commandService) : base(commandService)
	{
	}

	protected override ProcessStartInfo Info => new("/System/Library/PrivateFrameworks/Apple80211.framework/Versions/Current/Resources/airport", " -s");
}