using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;

namespace WiFiSurveyor.Core;

public sealed class SignalHub(IHubContext<SignalHub> context, IHostApplicationLifetime lifetime) : Hub, ISignalHub
{
	public override Task OnConnectedAsync()
	{
		if (lifetime.ApplicationStopping.IsCancellationRequested)
		{
			Context.Abort();
		}
		return base.OnConnectedAsync();
	}

	public async Task SendMessage(Message message)
		=> await context.Clients.All.SendAsync("Update", message);
}
