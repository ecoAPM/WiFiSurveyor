using System.Diagnostics;
using Hardware.Info;
using WiFiSurveyor.Core;

namespace WiFiSurveyor.Mac;

public sealed class MacSignalReader : PosixSignalReader
{
	public MacSignalReader(ICommandService commandService) : base(commandService)
	{
	}

	protected override ProcessStartInfo Info => new("system_profiler", "-json SPAirPortDataType");
}