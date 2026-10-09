using System.Diagnostics;
using WiFiSurveyor.Core;

namespace WiFiSurveyor.Mac;

public sealed class MacSignalReader(ICommandService commandService) : ISignalReader<string>
{
	public async Task<string> Read()
	{
		var outputFile = Path.GetTempFileName();
		try
		{
			var info = new ProcessStartInfo("/usr/bin/open");
			foreach (var argument in new[] { "-n", "-W", "-g", "--stdout", outputFile,
				Path.Combine(AppContext.BaseDirectory, "WiFiSurveyor.Scanner.app") })
				info.ArgumentList.Add(argument);
			await commandService.Run(info);
			var output = await File.ReadAllTextAsync(outputFile);
			return string.IsNullOrWhiteSpace(output)
				? throw new InvalidOperationException("The Wi-Fi scanner did not return data.")
				: output;
		}
		finally
		{
			File.Delete(outputFile);
		}
	}
}
