using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using NSubstitute;
using WiFiHeatMap.Server;
using Xunit;

namespace WiFiHeatMap.Tests
{
    public class SignalHubTests
    {
        [Fact]
        public async Task SendsUpdateToAllClients()
        {
            //arrange
            var clients = Substitute.For<IHubCallerClients>();
            clients.All.Returns(Substitute.For<IClientProxy>());
            
            var hub = new SignalHub { Clients = clients };

            //act
            await hub.SendMessage(new Message());

            //assert
            await hub.Clients.All.Received().SendCoreAsync("Update", Arg.Is<object[]>(array => array[0] is Message));
        }
    }
}