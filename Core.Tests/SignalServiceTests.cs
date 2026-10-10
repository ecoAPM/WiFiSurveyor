using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace WiFiSurveyor.Core.Tests;

public sealed class SignalServiceTests
{
	[Theory]
	[InlineData(false)]
	[InlineData(true)]
	public async Task ReportsCancellationOnlyWhenItWasNotRequested(bool canceled)
	{
		var reader = Substitute.For<ISignalReader<string>>();
		reader.When(r => r.Read()).Throw(new OperationCanceledException(new CancellationToken(canceled)));
		var hub = Substitute.For<ISignalHub>();
		var service = new SignalService<string>(reader, Substitute.For<ISignalParser<string>>(), hub, Substitute.For<ILogger>());

		await service.GetSignals();

		await hub.Received(canceled ? 0 : 1).SendMessage(Arg.Any<Message>());
	}

	[Fact]
	public async Task SetsStatusOnException()
	{
		//arrange
		var reader = Substitute.For<ISignalReader<string>>();
		reader.When(r => r.Read()).Throw(new Exception("unit test exception"));
		var parser = Substitute.For<ISignalParser<string>>();
		var hub = Substitute.For<ISignalHub>();
		var logger = Substitute.For<ILogger>();
		var service = new SignalService<string>(reader, parser, hub, logger);

		//act
		await service.GetSignals();

		//assert
		await hub.Received().SendMessage(Arg.Is<Message>(m => m != null && m.Status == "unit test exception"));
	}

	[Fact]
	public async Task StopsReadingOnStop()
	{
		//arrange
		var reader = Substitute.For<ISignalReader<string>>();
		var parser = Substitute.For<ISignalParser<string>>();
		var hub = Substitute.For<ISignalHub>();
		var logger = Substitute.For<ILogger>();
		var service = new SignalService<string>(reader, parser, hub, logger);
		var task = service.StartAsync(CancellationToken.None);

		//act
		await service.StopAsync(CancellationToken.None);
		await task;

		//assert
		Assert.True(task.IsCompletedSuccessfully);
	}

	[Fact]
	public async Task SetsSignalsOnUpdate()
	{
		//arrange
		var reader = Substitute.For<ISignalReader<string>>();
		var parser = Substitute.For<ISignalParser<string>>();
		var hub = Substitute.For<ISignalHub>();
		var signals = new List<Signal>
		{
			new()
			{
				SSID = "UnitTest",
				Frequency = Frequency._2_4_GHz,
				Strength = -30
			}
		};
		parser.Parse(Arg.Any<string>()).Returns(signals);
		var logger = Substitute.For<ILogger>();
		var service = new SignalService<string>(reader, parser, hub, logger);

		//act
		await service.GetSignals();

		//assert
		await hub.Received().SendMessage(Arg.Is<Message>(m => m != null && m.Signals.Equals(signals)));
	}
}
