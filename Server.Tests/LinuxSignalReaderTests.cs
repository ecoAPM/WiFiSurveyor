using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace WiFiSurveyor.Tests
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
        public async Task ReturnsDecentMessageWhenNotFound()
        {
            //arrange
            var exception = new Win32Exception(2, "x");
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
                Assert.Contains("running as root", e.Message);
            }
        }
    }
}