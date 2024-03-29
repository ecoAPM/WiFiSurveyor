using Microsoft.AspNetCore.SignalR;

namespace WiFiSurveyor.Core;

public sealed class SignalHub : Hub, ISignalHub
{
	private readonly IHubContext<SignalHub> _context;

	public SignalHub(IHubContext<SignalHub> context)
		=> _context = context;

	public async Task SendMessage(Message message)
		=> await _context.Clients.All.SendAsync("Update", message);
}