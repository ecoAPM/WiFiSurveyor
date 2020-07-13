using System.Diagnostics;
using System.Threading.Tasks;
using Xunit;

namespace WiFiHeatMap.Tests
{
    public class LinuxCommandServiceTests
    {
        [Fact]
        public async Task CanReadFromStdOut()
        {
            //arrange
            var service = new LinuxCommandService();

            //act
            var info = new ProcessStartInfo("ls");
            var output = await service.Run(info);

            //assert
            Assert.Contains("WiFiHeatMap.Tests.dll", output);
        }
    }
}
