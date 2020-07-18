using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace WiFiHeatMap.Server
{
    public interface ISignalHub
    {
        IHubCallerClients Clients {get; set;}
        Task SendMessage(Message message);
    }
}