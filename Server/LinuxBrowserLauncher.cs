namespace WiFiSurveyor;

public sealed class LinuxBrowserLauncher : BrowserLauncher
{
	public LinuxBrowserLauncher() : base("xdg-open")
	{
	}
}
