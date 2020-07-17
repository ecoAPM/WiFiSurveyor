using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace WiFiHeatMap
{
    public class SignalHub : Hub, ISignalHub
    {
        public async Task SendMessage(Message message)
            => await Clients.All.SendAsync("Update", message);
    }
}