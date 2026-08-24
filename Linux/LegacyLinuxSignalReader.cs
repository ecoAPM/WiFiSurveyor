using System.Diagnostics;
using WiFiSurveyor.Core;

namespace WiFiSurveyor.Linux;

public sealed class LegacyLinuxSignalReader(ICommandService commandService, IDeviceLocator deviceLocator) : PosixSignalReader(commandService)
{
	protected override ProcessStartInfo Info
		=> new("/sbin/iwlist", $"{Device} scanning");

	protected override string Package => "wireless-tools";

	private string Device => field ??= deviceLocator.GetDefaultDeviceName().GetAwaiter().GetResult();
}