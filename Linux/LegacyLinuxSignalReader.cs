using System.Diagnostics;
using WiFiSurveyor.Core;

namespace WiFiSurveyor.Linux;

public sealed class LegacyLinuxSignalReader(ICommandService commandService) : PosixSignalReader(commandService)
{
	protected override ProcessStartInfo Info
		=> new("/sbin/iwlist", $"{Device} scanning");

	protected override string Package => "wireless-tools";

	public string Device => field ??= GetDevice().GetAwaiter().GetResult();

	private async Task<string> GetDevice()
	{
		var info = new ProcessStartInfo("/sbin/iwconfig");
		var response = await commandService.Run(info);

		var regex = LegacyPatterns.Interface().Match(response);
		return regex.Groups[1].Value;
	}
}