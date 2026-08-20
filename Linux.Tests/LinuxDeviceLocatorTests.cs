using System.Diagnostics;
using NSubstitute;
using WiFiSurveyor.Core;
using Xunit;

namespace WiFiSurveyor.Linux.Tests;

public class LinuxDeviceLocatorTests
{
	[Fact]
	public async Task CanGetDefaultDeviceName()
	{
		//arrange
		var commandService = Substitute.For<ICommandService>();
		var locator = new LinuxDeviceLocator(commandService);

		commandService.Run(Arg.Any<ProcessStartInfo>()).Returns("Interface wlan123");

		//act
		var deviceName = await locator.GetDefaultDeviceName();

		//assert
		Assert.Equal("wlan123", deviceName);
	}
}