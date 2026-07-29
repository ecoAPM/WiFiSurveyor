using System.Diagnostics;
using WiFiSurveyor.Core;

namespace WiFiSurveyor.Windows;

public sealed class WindowsBrowserLauncher(Func<ProcessStartInfo, Process?> start) : BrowserLauncher(start, "cmd", "/c start");