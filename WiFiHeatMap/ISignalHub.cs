using System.Threading.Tasks;

namespace WiFiHeatMap
{
    public interface ISignalHub
    {
        Task SendMessage(Message message);
    }
}