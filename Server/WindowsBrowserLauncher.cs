namespace WiFiSurveyor;

public sealed class WindowsBrowserLauncher : BrowserLauncher
{
	public WindowsBrowserLauncher() : base("cmd", "/c start")
	{
	}
}
