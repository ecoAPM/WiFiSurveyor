using Microsoft.Extensions.DependencyInjection;
using WiFiSurveyor.Core;
using Xunit;

namespace WiFiSurveyor.Windows.Tests;

public class ProgramTests
{
	[Fact]
	public void AllServicesAreDefined()
	{
		//arrange
		var services = new ServiceCollection();

		//act
		services.AddWindowsHandlers();

		//assert
		var types = services.Select(s => s.ServiceType).ToArray();
		Assert.Contains(typeof(ISignalParser<IWiFiNetworkReport>), types);
		Assert.Contains(typeof(ISignalReader<IWiFiNetworkReport>), types);
		Assert.Contains(typeof(IBrowserLauncher), types);
	}
}