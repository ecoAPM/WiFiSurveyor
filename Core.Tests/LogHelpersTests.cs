using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace WiFiSurveyor.Core.Tests;

public sealed class LogHelpersTests
{
	[Fact]
	public void LogsIfEnabled()
	{
		//arrange
		var logger = Substitute.For<ILogger>();
		logger.IsEnabled(LogLevel.Debug).Returns(true);

		//act
		logger.LogIf(LogLevel.Debug, "test");

		//assert
		logger.Received().Log(LogLevel.Debug, "test");
	}

	[Fact]
	public void DoesNotLogIfNotEnabled()
	{
		//arrange
		var logger = Substitute.For<ILogger>();
		logger.IsEnabled(LogLevel.Debug).Returns(false);

		//act
		logger.LogIf(LogLevel.Debug, "test");

		//assert
		logger.DidNotReceive().Log(LogLevel.Debug, "test");
	}
}