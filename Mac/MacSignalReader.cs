using System.Diagnostics;
using WiFiSurveyor.Core;

namespace WiFiSurveyor.Mac;

public sealed class MacSignalReader(ICommandService commandService) : PosixSignalReader(commandService)
{
	protected override ProcessStartInfo Info => new("system_profiler", "SPAirPortDataType -detailLevel full -json");
}