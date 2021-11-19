using System.Threading.Tasks;

namespace WiFiSurveyor;

public interface ISignalHub
{
	Task SendMessage(Message message);
}