using System.Diagnostics;
using Xunit;

namespace WiFiSurveyor.Linux.Tests;

public sealed class LinuxBrowserLauncherTests
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

		var launcher = new LinuxBrowserLauncher(Start);

		//act
		launcher.Run("http://localhost");

		//assert
		Assert.Equal("xdg-open", info.FileName);
	}
}