using System.Diagnostics;
using NSubstitute;
using WiFiSurveyor.Core;
using Xunit;

namespace WiFiSurveyor.Linux.Tests;

public class LegacyLinuxDeviceLocatorTests
{
	[Fact]
	public async Task CanGetDefaultDeviceName()
	{
		//arrange
		var commandService = Substitute.For<ICommandService>();
		var locator = new LegacyLinuxDeviceLocator(commandService);

		commandService.Run(Arg.Any<ProcessStartInfo>()).Returns("wlan123 IEEE 802.11");

		//act
		var deviceName = await locator.GetDefaultDeviceName();

		//assert
		Assert.Equal("wlan123", deviceName);
	}
}