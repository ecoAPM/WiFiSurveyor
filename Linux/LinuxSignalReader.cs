using System.Diagnostics;
using WiFiSurveyor.Core;

namespace WiFiSurveyor.Linux;

public sealed class LinuxSignalReader : PosixSignalReader
{
	protected override ProcessStartInfo Info => new ProcessStartInfo("iwlist", "wlan0 scanning");

	public LinuxSignalReader(ICommandService commandService) : base(commandService)
	{
	}
}