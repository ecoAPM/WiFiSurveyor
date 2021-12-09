using WiFiSurveyor.Core;

namespace WiFiSurveyor.Mac;

public sealed class MacBrowserLauncher : BrowserLauncher
{
	public MacBrowserLauncher() : base("open")
	{
	}
}