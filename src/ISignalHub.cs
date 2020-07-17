using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace WiFiHeatMap
{
    public interface ISignalHub
    {
        IHubCallerClients Clients {get; set;}
        Task SendMessage(Message message);
    }
}