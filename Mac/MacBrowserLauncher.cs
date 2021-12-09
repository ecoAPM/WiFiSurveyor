using System.Diagnostics;
using WiFiSurveyor.Core;

namespace WiFiSurveyor.Mac;

public sealed class MacBrowserLauncher : BrowserLauncher
{
	public MacBrowserLauncher(Func<ProcessStartInfo, Process?> start) : base(start, "open")
	{
	}
}