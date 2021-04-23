using KaggleReader.Library.DI;
using KaggleReader.Library.Services;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace KaggleReader.Tests.Tests.EurovisionHandler
{
    public class EurovisionHandlerTests
    {
        private readonly IEurovisionHandler _eurovisionHandler;

        public EurovisionHandlerTests()
        {
            _eurovisionHandler = ServiceFactory.GetService<IEurovisionHandler>();
        }

        [Fact]
        public async Task GetLyricsAsync()
        {
            var lyrics = await _eurovisionHandler.GetLyricsAsync();
            Assert.NotNull(lyrics);
            Assert.True(lyrics.Any());
        }
    }
}
