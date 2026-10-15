using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSubstitute;
using Xunit;

namespace WiFiSurveyor.Core.Tests;

public sealed class AppTests
{
	[Fact]
	public async Task ServerCanStartAndStopAfterConstruction()
	{
		var started = false;
		var app = new App(services => services.AddSingleton<IHostedService>(provider =>
		{
			var lifetime = provider.GetRequiredService<IHostApplicationLifetime>();
			lifetime.ApplicationStarted.Register(() =>
			{
				started = true;
				lifetime.StopApplication();
			});
			return Substitute.For<IHostedService>();
		}), ["dev", "--urls=http://127.0.0.1:0"]);

		await app.Run().WaitAsync(TimeSpan.FromSeconds(10));

		Assert.True(started);
	}

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
