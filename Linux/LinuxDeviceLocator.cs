using System.Diagnostics;
using WiFiSurveyor.Core;

namespace WiFiSurveyor.Linux;

public sealed class LinuxDeviceLocator(ICommandService commandService) : IDeviceLocator
{
	public async Task<string> GetDefaultDeviceName()
	{
		var info = new ProcessStartInfo("/usr/sbin/iw", "dev");
		var response = await commandService.Run(info);

		var regex = Patterns.Interface().Match(response);
		var device = regex.Groups[1].Value;

		return !string.IsNullOrWhiteSpace(device)
			? device
			: throw new ArgumentException("No Wi-Fi device found!");
	}
}