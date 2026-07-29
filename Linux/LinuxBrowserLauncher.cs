using System.Diagnostics;
using WiFiSurveyor.Core;

namespace WiFiSurveyor.Linux;

public sealed class LinuxBrowserLauncher(Func<ProcessStartInfo, Process?> start) : BrowserLauncher(start, "xdg-open");