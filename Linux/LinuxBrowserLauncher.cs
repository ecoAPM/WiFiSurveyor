using System.Diagnostics;
using WiFiSurveyor.Core;

namespace WiFiSurveyor.Linux;

public sealed class LinuxBrowserLauncher : BrowserLauncher
{
	public LinuxBrowserLauncher(Func<ProcessStartInfo, Process?> start) : base(start, "xdg-open")
	{
	}
}