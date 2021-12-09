namespace WiFiSurveyor.Core;

public interface ISignalHub
{
	Task SendMessage(Message message);
}