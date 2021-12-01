using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using NSubstitute;
using Xunit;

namespace WiFiSurveyor.Tests;

public sealed class SignalHubTests
{
	[Fact]
	public async Task SendsUpdateToAllClients()
	{
		//arrange
		var context = Substitute.For<IHubContext<SignalHub>>();
		context.Clients.All.Returns(Substitute.For<IClientProxy>());

		var hub = new SignalHub(context);

		//act
		await hub.SendMessage(new Message());

		//assert
		await context.Clients.All.Received().SendCoreAsync("Update", Arg.Is<object[]>(array => array[0] is Message));
	}
}