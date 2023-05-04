using NSubstitute;
using Xunit;

namespace WiFiSurveyor.Windows.Tests;

public sealed class WindowsSignalReaderTests
{
	[Fact]
	public async Task ReturnsOutputFromProcess()
	{
		//arrange
		var report = Substitute.For<IWiFiNetworkReport>();
		var adapter = Substitute.For<IWiFiAdapter>();
		adapter.NetworkReport.Returns(report);
		var reader = new WindowsSignalReader(() => Task.FromResult(adapter));

		//act
		var results = await reader.Read();

		//assert
		Assert.Equal(report, results);
	}
}