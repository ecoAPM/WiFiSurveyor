using System.Runtime.InteropServices;

namespace WiFiSurveyor.Mac;

internal static class MacApplication
{
	internal static void Run(Func<Task> runServer)
	{
		NativeLibrary.Load("/System/Library/Frameworks/AppKit.framework/AppKit");
		NativeLibrary.Load("/System/Library/Frameworks/CoreLocation.framework/CoreLocation");
		var coreFoundation = NativeLibrary.Load(CoreFoundation);
		var mode = Marshal.ReadIntPtr(NativeLibrary.GetExport(coreFoundation, "kCFRunLoopDefaultMode"));
		var pool = Send(Class("NSAutoreleasePool"), Selector("new"));
		var location = nint.Zero;
		try
		{
			// CoreWLAN checks the app identity registered by NSApplication.
			Send(Class("NSApplication"), Selector("sharedApplication"));
			location = Send(Class("CLLocationManager"), Selector("new"));
			SendVoid(location, Selector("requestWhenInUseAuthorization"));

			WaitForServer(Task.Run(runServer), () =>
			{
				var iterationPool = Send(Class("NSAutoreleasePool"), Selector("new"));
				try
				{
					RunLoop(mode, 1, false);
				}
				finally
				{
					SendVoid(iterationPool, Selector("drain"));
				}
			});
		}
		finally
		{
			SendVoid(location, Selector("release"));
			SendVoid(pool, Selector("drain"));
		}
	}

	internal static void WaitForServer(Task server, Action runLoop)
	{
		while (!server.IsCompleted)
		{
			runLoop();
		}
		server.GetAwaiter().GetResult();
	}

	private const string ObjC = "/usr/lib/libobjc.A.dylib";
	private const string CoreFoundation = "/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation";

	[DllImport(ObjC, EntryPoint = "objc_getClass")]
	private static extern nint Class([MarshalAs(UnmanagedType.LPUTF8Str)] string name);

	[DllImport(ObjC, EntryPoint = "sel_registerName")]
	private static extern nint Selector([MarshalAs(UnmanagedType.LPUTF8Str)] string name);

	[DllImport(ObjC, EntryPoint = "objc_msgSend")]
	private static extern nint Send(nint obj, nint selector);

	[DllImport(ObjC, EntryPoint = "objc_msgSend")]
	private static extern void SendVoid(nint obj, nint selector);

	[DllImport(CoreFoundation, EntryPoint = "CFRunLoopRunInMode")]
	private static extern int RunLoop(nint mode, double seconds, [MarshalAs(UnmanagedType.I1)] bool returnAfterSourceHandled);
}
