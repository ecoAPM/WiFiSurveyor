using System.Diagnostics;
using WiFiSurveyor.Core;

namespace WiFiSurveyor.Linux;

public sealed class LinuxSignalReader : PosixSignalReader
{
	protected override ProcessStartInfo Info => new("iwlist", "wlan0 scanning");

	public LinuxSignalReader(ICommandService commandService) : base(commandService)
	{
	}
}