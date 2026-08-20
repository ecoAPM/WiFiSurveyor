namespace WiFiSurveyor.Core;

public interface IDeviceLocator
{
	Task<string> GetDefaultDeviceName();
}