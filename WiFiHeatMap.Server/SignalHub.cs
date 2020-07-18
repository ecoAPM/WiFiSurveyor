using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace WiFiHeatMap.Server
{
    public class SignalHub : Hub, ISignalHub
    {
        public async Task SendMessage(Message message) => await
        (
            Clients != null
                ? Clients.All.SendAsync("Update", message)
                : Task.CompletedTask
        );
    }
}