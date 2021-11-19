using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace WiFiSurveyor;

public class SignalHub : Hub, ISignalHub
{
	private readonly IHubContext<SignalHub> _context;

	public SignalHub(IHubContext<SignalHub> context)
		=> _context = context;

	public async Task SendMessage(Message message)
		=> await _context.Clients.All.SendAsync("Update", message);
}