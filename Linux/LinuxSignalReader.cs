using System.Diagnostics;
using WiFiSurveyor.Core;

namespace WiFiSurveyor.Linux;

public sealed class LinuxSignalReader : PosixSignalReader
{
	public LinuxSignalReader(ICommandService commandService) : base(commandService)
	{
	}

	protected override ProcessStartInfo Info
		=> new("/sbin/iwlist", "wlan0 scanning");
}