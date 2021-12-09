using System.Diagnostics;
using Xunit;

namespace WiFiSurveyor.Mac.Tests;

public sealed class MacBrowserLauncherTests
{
	[Fact]
	public void RunsCorrectProgram()
	{
		//arrange
		ProcessStartInfo info = null!;

		Process? Start(ProcessStartInfo i)
		{
			info = i;
			return null;
		}
		var launcher = new MacBrowserLauncher(Start);

		//act
		launcher.Run("http://localhost");

		//assert
		Assert.Equal("open", info.FileName);
	}
}