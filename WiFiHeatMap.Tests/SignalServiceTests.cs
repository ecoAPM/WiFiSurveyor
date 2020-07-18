using System;
using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using WiFiHeatMap.Server;
using Xunit;

namespace WiFiHeatMap.Tests
{
    public class SignalServiceTests
    {
        [Fact]
        public async Task SetsStatusOnException()
        {
            //arrange
            var reader = Substitute.For<ISignalReader>();
            reader.When(r => r.Read()).Throw(new Exception("unit test exception"));
            var parser = Substitute.For<ISignalParser>();
            var hub = Substitute.For<ISignalHub>();
            var service = new SignalService(reader, parser, hub);

            //act
            await service.StartAsync(CancellationToken.None);

            //assert
            await hub.Received().SendMessage(Arg.Is<Message>(m => m.Status == "unit test exception"));
        }

        [Fact]
        public async Task StopsReadingOnStop()
        {
            //arrange
            var reader = Substitute.For<ISignalReader>();
            var parser = Substitute.For<ISignalParser>();
            var hub = Substitute.For<ISignalHub>();
            var service = new SignalService(reader, parser, hub);

            //act
            var task = service.StartAsync(CancellationToken.None);
            await service.StopAsync(CancellationToken.None);
            await task;

            //assert
            Assert.True(task.IsCompleted);
        }

        [Fact]
        public async Task SetsSignalsOnUpdate()
        {
            //arrange
            var reader = Substitute.For<ISignalReader>();
            var parser = Substitute.For<ISignalParser>();
            var hub = Substitute.For<ISignalHub>();
            var signals = new[]
            {
                new Signal
                {
                    SSID = "UnitTest",
                    Frequency = Frequency._2_4_GHz,
                    Strength = -30
                }
            };
            parser.Parse(Arg.Any<string>()).Returns(signals);
            var service = new SignalService(reader, parser, hub);

            //act
            var task = service.StartAsync(CancellationToken.None);
            await task;

            //assert
            await hub.Received().SendMessage(Arg.Is<Message>(m => m.Signals == signals));
        }
    }
}