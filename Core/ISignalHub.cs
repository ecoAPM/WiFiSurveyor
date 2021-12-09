using System.Threading.Tasks;

namespace WiFiSurveyor.Core;

public interface ISignalHub
{
	Task SendMessage(Message message);
}