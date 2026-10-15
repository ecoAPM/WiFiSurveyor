using System.Runtime.InteropServices;

namespace WiFiSurveyor.Mac;

internal static class CoreWlan
{
	static CoreWlan()
	{
		NativeLibrary.Load("/System/Library/Frameworks/Foundation.framework/Foundation");
		NativeLibrary.Load("/System/Library/Frameworks/CoreWLAN.framework/CoreWLAN");
	}

	internal static IReadOnlyList<WiFiNetwork>? Scan()
	{
		var pool = Send(Send(GetClass("NSAutoreleasePool"), Selector("alloc")), Selector("init"));
		try
		{
			var wifi = Send(Send(GetClass("CWWiFiClient"), Selector("sharedWiFiClient")), Selector("interface"));
			if (wifi == 0 || !GetBool(wifi, Selector("powerOn")))
			{
				return null;
			}

			var results = ScanNetworks(wifi, Selector("scanForNetworksWithSSID:includeHidden:error:"), 0, false, out var error);
			// Cocoa reports failure through a nil result, not the NSError output alone.
			if (results == 0)
			{
				throw new InvalidOperationException(GetString(error, "localizedDescription") ?? "macOS did not return Wi-Fi scan results.");
			}

			var array = Send(results, Selector("allObjects"));
			var count = Send(array, Selector("count"));
			var networks = new List<WiFiNetwork>(checked((int)count));
			for (nint i = 0; i < count; i++)
			{
				var network = Send(array, Selector("objectAtIndex:"), i);
				var channel = Send(network, Selector("wlanChannel"));
				networks.Add(new(GetString(network, "ssid"), GetString(network, "bssid"),
					Send(network, Selector("rssiValue")), Send(channel, Selector("channelNumber")), Send(channel, Selector("channelBand"))));
			}
			return networks;
		}
		finally
		{
			SendVoid(pool, Selector("drain"));
		}
	}

	private const string ObjC = "/usr/lib/libobjc.A.dylib";

	[DllImport(ObjC, EntryPoint = "objc_getClass")]
	private static extern nint GetClass([MarshalAs(UnmanagedType.LPUTF8Str)] string name);

	[DllImport(ObjC, EntryPoint = "sel_registerName")]
	private static extern nint Selector([MarshalAs(UnmanagedType.LPUTF8Str)] string name);

	[DllImport(ObjC, EntryPoint = "objc_msgSend")]
	private static extern nint Send(nint obj, nint selector);

	[DllImport(ObjC, EntryPoint = "objc_msgSend")]
	private static extern nint Send(nint obj, nint selector, nint argument);

	[DllImport(ObjC, EntryPoint = "objc_msgSend")]
	private static extern void SendVoid(nint obj, nint selector);

	[DllImport(ObjC, EntryPoint = "objc_msgSend")]
	[return: MarshalAs(UnmanagedType.I1)]
	private static extern bool GetBool(nint obj, nint selector);

	[DllImport(ObjC, EntryPoint = "objc_msgSend")]
	private static extern nint ScanNetworks(nint obj, nint selector, nint ssid,
		[MarshalAs(UnmanagedType.I1)] bool includeHidden, out nint error);

	private static string? GetString(nint obj, string property)
	{
		var value = Send(obj, Selector(property));
		return value == 0 ? null : Marshal.PtrToStringUTF8(Send(value, Selector("UTF8String")),
			checked((int)Send(value, Selector("lengthOfBytesUsingEncoding:"), 4)));
	}
}
