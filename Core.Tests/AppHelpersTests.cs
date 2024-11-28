using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSubstitute;
using Xunit;

namespace WiFiSurveyor.Core.Tests;

public sealed class AppHelpersTests
{
	private static readonly ICollection<string> Addresses = ["http://localhost:1234"];

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
	public async Task LaunchesBrowserInProd()
	{
		//arrange
		var env = Substitute.For<IWebHostEnvironment>();
		env.EnvironmentName.Returns(Environments.Production);

		var launcher = Substitute.For<IBrowserLauncher>();

		var feature = Substitute.For<IServerAddressesFeature>();
		feature.Addresses.Returns(Addresses);

		var server = Substitute.For<IServer>();
		server.Features.Get<IServerAddressesFeature>().Returns(feature);

		var app = Substitute.For<IHost>();
		app.Services.GetService<IBrowserLauncher>().Returns(launcher);
		app.Services.GetService<IServer>().Returns(server);

		//act
		_ = app.Run(env);

		//assert
		await app.StopAsync();
		launcher.Received().Run(Arg.Any<string>());
	}

	[Fact]
	public async Task DoesNotLaunchBrowserInDev()
	{
		//arrange
		var env = Substitute.For<IWebHostEnvironment>();
		env.EnvironmentName.Returns(Environments.Development);

		var launcher = Substitute.For<IBrowserLauncher>();

		var feature = Substitute.For<IServerAddressesFeature>();
		feature.Addresses.Returns(Addresses);

		var server = Substitute.For<IServer>();
		server.Features.Get<IServerAddressesFeature>().Returns(feature);

		var app = Substitute.For<IHost>();
		app.Services.GetService<IBrowserLauncher>().Returns(launcher);
		app.Services.GetService<IServer>().Returns(server);

		//act
		_ = app.Run(env);

		//assert
		await app.StopAsync();
		launcher.DidNotReceive().Run(Arg.Any<string>());
	}
}