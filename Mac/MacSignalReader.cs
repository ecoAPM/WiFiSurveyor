using System.Diagnostics;
using WiFiSurveyor.Core;

namespace WiFiSurveyor.Mac;

public sealed class MacSignalReader(ICommandService commandService) : PosixSignalReader(commandService)
{
	protected override ProcessStartInfo Info
		=> new("/usr/sbin/system_profiler", "SPAirPortDataType -detailLevel full -json");

	protected override string Package => "system_profiler";
}