using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using WiFiSurveyor.Core;
using Xunit;

namespace WiFiSurveyor.Mac.Tests;

public class ProgramTests
{
	[Fact]
	public void AllServicesAreDefined()
	{
		//arrange
		var services = new ServiceCollection();

		//act
		services.AddMacHandlers();

		//assert
		var types = services.Select(s => s.ServiceType).ToArray();
		Assert.Contains(typeof(ISignalParser<IReadOnlyList<IWiFiNetwork>>), types);
		Assert.Contains(typeof(ISignalReader<IReadOnlyList<IWiFiNetwork>>), types);
		Assert.Contains(typeof(IBrowserLauncher), types);
	}
}