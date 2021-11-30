using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.Hosting;

namespace WiFiSurveyor;

public static class Program
{
	public static async Task Main(string[] args)
	{
		var host = WebHost.CreateDefaultBuilder()
			.UseStartup<Startup>()
			.UseUrls("http://127.0.0.1:0")
			.Build();

		host.Start();

		var binding = host.ServerFeatures.Get<IServerAddressesFeature>();
		var address = binding.Addresses.FirstOrDefault();
		Process.Start(address);

		await host.WaitForShutdownAsync();
	}
}