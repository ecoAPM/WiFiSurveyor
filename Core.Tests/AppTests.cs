using Microsoft.AspNetCore.Hosting;
using Xunit;

namespace WiFiSurveyor.Core.Tests;

public sealed class AppTests
{
	[Fact]
	public void CanConfigureApp()
	{
		//arrange
		var args = Array.Empty<string>();

		//act
		var app = new App(_ => { }, args);

		//assert
		Assert.IsType<App>(app);
	}
}