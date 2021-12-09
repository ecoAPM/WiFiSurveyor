using System.Diagnostics;
using System.Threading.Tasks;
using NSubstitute;
using WiFiSurveyor.Core;
using Xunit;

namespace WiFiSurveyor.Mac.Tests;

public sealed class MacSignalReaderTests
{
	[Fact]
	public async Task ReturnsOutputFromProcess()
	{
		//arrange
		var commandService = Substitute.For<ICommandService>();
		commandService.Run(Arg.Any<ProcessStartInfo>()).Returns("file contents");
		var reader = new MacSignalReader(commandService);

		//act
		var results = await reader.Read();

		//assert
		Assert.Equal("file contents", results);
	}
}