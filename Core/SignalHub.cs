using Microsoft.AspNetCore.SignalR;

namespace WiFiSurveyor.Core;

public sealed class SignalHub(IHubContext<SignalHub> context) : Hub, ISignalHub
{
	public async Task SendMessage(Message message)
		=> await context.Clients.All.SendAsync("Update", message);
}