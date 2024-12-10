using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NSubstitute;
using WiFiSurveyor.Core;
using Xunit;

namespace WiFiSurveyor.Mac.Tests;

public sealed class MacSignalParserTests
{
	[Fact]
	public async Task ResultsAreParsedIntoSignals()
	{
		//arrange
		var logger = Substitute.For<ILogger>();
		var signalParser = new MacSignalParser(logger);
		var contents = await File.ReadAllTextAsync("system_profiler-output.txt");

		//act
		var signals = signalParser.Parse(contents).ToList();

		//assert
		Assert.Equal("net1", signals[0].SSID);
		Assert.Empty(signals[0].MAC);
		Assert.Equal(Frequency._2_4_GHz, signals[0].Frequency);
		Assert.Equal(9, signals[0].Channel);
		Assert.Equal(-39, signals[0].Strength);

		Assert.Equal("ssid🏎2", signals[1].SSID);
		Assert.Empty(signals[1].MAC);
		Assert.Equal(Frequency._5_GHz, signals[1].Frequency);
		Assert.Equal(44, signals[1].Channel);
		Assert.Equal(-50, signals[1].Strength);

		Assert.Equal("access_point_3", signals[2].SSID);
		Assert.Empty(signals[2].MAC);
		Assert.Equal(Frequency._5_GHz, signals[2].Frequency);
		Assert.Equal(149, signals[2].Channel);
		Assert.Equal(-42, signals[2].Strength);

		Assert.Equal("wap-4", signals[3].SSID);
		Assert.Empty(signals[3].MAC);
		Assert.Equal(Frequency._2_4_GHz, signals[3].Frequency);
		Assert.Equal(11, signals[3].Channel);
		Assert.Equal(-90, signals[3].Strength);
	}
}