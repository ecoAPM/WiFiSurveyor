using System.Diagnostics;
using WiFiSurveyor.Core;

namespace WiFiSurveyor.Linux;

public sealed class LegacyLinuxSignalReader(ICommandService commandService) : PosixSignalReader(commandService)
{
	protected override ProcessStartInfo Info
		=> new("/sbin/iwlist", "wlan0 scanning");
}