using System.Diagnostics;
using WiFiSurveyor.Core;

namespace WiFiSurveyor.Linux;

public sealed class LegacyLinuxDeviceLocator(ICommandService commandService) : IDeviceLocator
{
	public async Task<string> GetDefaultDeviceName()
	{
		var info = new ProcessStartInfo("/usr/sbin/iwconfig");
		var response = await commandService.Run(info);

		var regex = LegacyPatterns.Interface().Match(response);
		return regex.Groups[1].Value;
	}
}