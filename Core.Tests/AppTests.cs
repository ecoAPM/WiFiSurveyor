using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
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

public sealed class AppHelpersTests
{
	[Fact]
	public void CanRegisterSharedServices()
	{
		//arrange
		var services = new ServiceCollection();

		//act
		services.AddPosixHandlers();

		//assert
		var types = services.Select(s => s.ImplementationType).ToArray();
		Assert.Contains(typeof(CommandService), types);
		Assert.Contains(typeof(SignalService<string>), types);
	}

	[Fact]
	public void CanLaunchBrowser()
	{
		//arrange
		var launcher = Substitute.For<IBrowserLauncher>();

		var builder = WebApplication.CreateBuilder();
		builder.Services.AddSingleton(launcher);
		var app = builder.Build();
		app.Urls.Add("http://localhost:1234");

		//act
		app.LaunchBrowser();

		//assert
		launcher.Received().Run("http://localhost:1234");
	}
}