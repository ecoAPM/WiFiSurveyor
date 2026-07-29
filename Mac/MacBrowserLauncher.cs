using System.Diagnostics;
using WiFiSurveyor.Core;

namespace WiFiSurveyor.Mac;

public sealed class MacBrowserLauncher(Func<ProcessStartInfo, Process?> start) : BrowserLauncher(start, "open");