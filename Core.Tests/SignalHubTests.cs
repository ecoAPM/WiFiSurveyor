using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using NSubstitute;
using Xunit;

namespace WiFiSurveyor.Core.Tests;

public sealed class SignalHubTests
{
	[Fact]
	public async Task SendsUpdateToAllClients()
	{
		//arrange
		var context = Substitute.For<IHubContext<SignalHub>>();
		context.Clients.All.Returns(Substitute.For<IClientProxy>());

		var hub = new SignalHub(context, Substitute.For<IHostApplicationLifetime>());

		//act
		await hub.SendMessage(new Message());

		//assert
		await context.Clients.All.Received().SendCoreAsync("Update", Arg.Is<object[]>(array => array != null && array[0] is Message));
	}

	[Theory]
	[InlineData(false)]
	[InlineData(true)]
	public async Task AcceptsConnectionsOnlyBeforeShutdown(bool stopping)
	{
		var lifetime = Substitute.For<IHostApplicationLifetime>();
		lifetime.ApplicationStopping.Returns(new CancellationToken(stopping));
		var caller = Substitute.For<HubCallerContext>();
		var hub = new SignalHub(Substitute.For<IHubContext<SignalHub>>(), lifetime) { Context = caller };

		await hub.OnConnectedAsync();

		caller.Received(stopping ? 1 : 0).Abort();
	}
}
