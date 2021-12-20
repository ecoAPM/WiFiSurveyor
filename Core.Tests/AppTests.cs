using Xunit;

namespace WiFiSurveyor.Core.Tests;

public sealed class AppTests
{
	[Theory]
	[InlineData("dev", "Development")]
	[InlineData("", "Production")]
	public void DevFlagSetsEnvironment(string arg, string expected)
	{
		//arrange
		var args = new[] { arg };

		//act
		var app = new App(_ => { }, args);

		//assert
		Assert.Equal(expected, app.Environment.EnvironmentName);
	}
}