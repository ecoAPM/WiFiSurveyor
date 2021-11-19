using System;
using System.Diagnostics;

namespace WiFiSurveyor;

public class LinuxSignalReader : PosixSignalReader
{
	protected override ProcessStartInfo Info => new ProcessStartInfo("iwlist", "wlan0 scanning");

	public LinuxSignalReader(ICommandService commandService) : base(commandService)
	{
	}
}