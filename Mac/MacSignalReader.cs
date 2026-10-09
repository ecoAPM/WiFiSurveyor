using System.Diagnostics;
using WiFiSurveyor.Core;

namespace WiFiSurveyor.Mac;

public sealed class MacSignalReader(ICommandService commandService) : ISignalReader<string>
{
	public async Task<string> Read()
		=> await commandService.Run(new ProcessStartInfo(
			Path.Combine(AppContext.BaseDirectory, "WiFiSurveyor.Scanner")));
}
