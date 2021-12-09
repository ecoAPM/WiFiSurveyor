namespace WiFiSurveyor.Core;

public static class LogHelpers
{
	public static void LogIf(this ILogger logger, LogLevel level, string message, params object[] args)
	{
		if (logger.IsEnabled(level))
		{
#pragma warning disable CA2254
			logger.Log(level, message, args);
#pragma warning restore CA2254
		}
	}
}