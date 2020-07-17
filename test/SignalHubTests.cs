using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using NSubstitute;
using Xunit;

namespace WiFiHeatMap.Tests
{
    public class SignalHubTests
    {
        [Fact]
        public async Task SendsUpdateToAllClients()
        {
            //arrange
            var hub = new SignalHub();
            hub.Clients = Substitute.For<IHubCallerClients>();
            hub.Clients.All.Returns(Substitute.For<IClientProxy>());

            //act
            await hub.SendMessage(new Message());

            //assert
            await hub.Clients.All.Received().SendCoreAsync("Update", Arg.Is<object[]>(array => array[0] is Message));
        }
    }
}