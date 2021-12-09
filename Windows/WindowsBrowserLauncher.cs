using System.Diagnostics;
using WiFiSurveyor.Core;

namespace WiFiSurveyor.Windows;

public sealed class WindowsBrowserLauncher : BrowserLauncher
{
	public WindowsBrowserLauncher(Func<ProcessStartInfo, Process?> start) : base(start, "cmd", "/c start")
	{
	}
}