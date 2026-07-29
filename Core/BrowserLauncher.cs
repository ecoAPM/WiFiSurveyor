using System.Diagnostics;

namespace WiFiSurveyor.Core;

public abstract class BrowserLauncher(Func<ProcessStartInfo, Process?> start, string command, string baseArgs = "") : IBrowserLauncher
{
	public void Run(string url)
		=> start(new ProcessStartInfo(command, $"{baseArgs} {url}".Trim()));
}