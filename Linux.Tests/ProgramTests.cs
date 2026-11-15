using Microsoft.Extensions.DependencyInjection;
using WiFiSurveyor.Core;
using Xunit;

namespace WiFiSurveyor.Linux.Tests;

public class ProgramTests
{
	[Fact]
	public void AllServicesAreDefined()
	{
		//arrange
		var services = new ServiceCollection();

		//act
		services.AddLinuxHandlers();

		//assert
		var types = services.Select(s => s.ServiceType);
		Assert.Contains(typeof(IDeviceLocator), types);
		Assert.Contains(typeof(ISignalParser<string>), types);
		Assert.Contains(typeof(ISignalReader<string>), types);
		Assert.Contains(typeof(IBrowserLauncher), types);
	}
}