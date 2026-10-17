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
		//arrange
		var app = new App(AddHandlers, []);

		//act
		var task = app.Run();
		await task;

		//assert
		Assert.True(task.IsCompletedSuccessfully);
	}

	private static void AddHandlers(IServiceCollection s)
	{
		s.AddSingleton(Substitute.For<IBrowserLauncher>());
		s.AddSingleton(HostedService);
	}

	private static IHostedService HostedService(IServiceProvider serviceProvider)
	{
		var lifetime = serviceProvider.GetRequiredService<IHostApplicationLifetime>();
		lifetime.ApplicationStarted.Register(lifetime.StopApplication);
		return Substitute.For<IHostedService>();
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