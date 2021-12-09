using WiFiSurveyor.Core;

namespace WiFiSurveyor.Linux;

public sealed class LinuxBrowserLauncher : BrowserLauncher
{
	public LinuxBrowserLauncher() : base("xdg-open")
	{
	}
}