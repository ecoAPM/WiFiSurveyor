using WiFiSurveyor.Core;

namespace WiFiSurveyor.Windows;

public sealed class WindowsBrowserLauncher : BrowserLauncher
{
	public WindowsBrowserLauncher() : base("cmd", "/c start")
	{
	}
}
