using System.Diagnostics;
using WiFiSurveyor.Core;

namespace WiFiSurveyor.Linux;

public sealed class LinuxSignalReader(ICommandService commandService) : PosixSignalReader(commandService)
{
	protected override ProcessStartInfo Info
		=> new("/sbin/iw", $"dev {Device} scan");

	protected override string Package => "iw";

	public string Device => field ??= GetDevice().GetAwaiter().GetResult();

	private async Task<string> GetDevice()
	{
		var info = new ProcessStartInfo("/sbin/iw", "dev");
		var response = await commandService.Run(info);

		var regex = Patterns.Interface().Match(response);
		return regex.Groups[1].Value;
	}
}