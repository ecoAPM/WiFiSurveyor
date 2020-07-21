using System.Threading.Tasks;

namespace WiFiHeatMap.Server
{
    public interface ISignalHub
    {
        Task SendMessage(Message message);
    }
}