using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using NSubstitute;
using WiFiHeatMap.Server;
using Xunit;

namespace WiFiHeatMap.Tests
{
    public class LinuxSignalReaderTests
    {
        [Fact]
        public async Task ReturnsOutputFromProcess()
        {
            //arrange
            var commandService = Substitute.For<ICommandService>();
            commandService.Run(Arg.Any<ProcessStartInfo>()).Returns("file contents");
            var reader = new LinuxSignalReader(commandService);

            //act
            var results = await reader.Read();

            //assert
            Assert.Equal("file contents", results);
        }

        [Fact]
        public async Task ReturnsDecentMessageWhenNotInstalled()
        {
            //arrange
            var exception = new ExternalException("x", 2);
            var commandService = Substitute.For<ICommandService>();
            commandService.When(c => c.Run(Arg.Any<ProcessStartInfo>())).Throw(exception);
            var reader = new LinuxSignalReader(commandService);

            try
            {
                //act
                var results = await reader.Read();
            }
            catch (Exception e)
            {
                //assert
                Assert.Contains("\"wireless-tools\" is installed", e.Message);
            }
        }

        [Fact]
        public async Task ReturnsDecentMessageWhenNotRunningAsRoot()
        {
            //arrange
            var exception = new ExternalException("x", 13);
            var commandService = Substitute.For<ICommandService>();
            commandService.When(c => c.Run(Arg.Any<ProcessStartInfo>())).Throw(exception);
            var reader = new LinuxSignalReader(commandService);

            try
            {
                //act
                var results = await reader.Read();
            }
            catch (Exception e)
            {
                //assert
                Assert.Contains("run as root", e.Message);
            }
        }
    }
}