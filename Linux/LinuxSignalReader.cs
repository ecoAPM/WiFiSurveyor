using System.Diagnostics;
using WiFiSurveyor.Core;

namespace WiFiSurveyor.Linux;

public sealed class LinuxSignalReader(ICommandService commandService, IDeviceLocator device) : PosixSignalReader(commandService)
{
	protected override ProcessStartInfo Info
		=> new("/usr/sbin/iw", $"dev {Device} scan flush");

	protected override string Package => "iw";

	private string Device => field ??= device.GetDefaultDeviceName().GetAwaiter().GetResult();
}