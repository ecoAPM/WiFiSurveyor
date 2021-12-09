using System.Diagnostics;
using Xunit;

namespace WiFiSurveyor.Windows.Tests;

public sealed class WindowsBrowserLauncherTests
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
		var launcher = new WindowsBrowserLauncher(Start);

		//act
		launcher.Run("http://localhost");

		//assert
		Assert.Equal("cmd", info.FileName);
	}
}