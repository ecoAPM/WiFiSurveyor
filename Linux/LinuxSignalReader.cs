using System.Diagnostics;
using WiFiSurveyor.Core;

namespace WiFiSurveyor.Linux;

public sealed class LinuxSignalReader(ICommandService commandService) : PosixSignalReader(commandService)
{
	protected override ProcessStartInfo Info
		=> new("iw", $"dev {Device} scan flush");

	protected override string Package => "iw";

	private string Device => field ??= GetDevice().GetAwaiter().GetResult();

	private async Task<string> GetDevice()
	{
		var info = new ProcessStartInfo("iw", "dev");
		var response = await commandService.Run(info);

		var regex = Patterns.Interface().Match(response);
		var device = regex.Groups[1].Value;

		return !string.IsNullOrWhiteSpace(device)
			? device
			: throw new ArgumentException("No Wi-Fi device found!");
	}
}