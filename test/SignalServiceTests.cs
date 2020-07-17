using System;
using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
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
            var service = new SignalService(reader, parser);

            //act
            await service.StartAsync(CancellationToken.None);

            //assert
            Assert.Equal("unit test exception", service.Status);
        }

        [Fact]
        public async Task StopsReadingOnStop()
        {
            //arrange
            var reader = Substitute.For<ISignalReader>();
            var parser = Substitute.For<ISignalParser>();
            var service = new SignalService(reader, parser);

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
            var service = new SignalService(reader, parser);

            //act
            var task = service.StartAsync(CancellationToken.None);
            await task;

            //assert
            Assert.Equal(signals, service.Signals);
        }
    }
}