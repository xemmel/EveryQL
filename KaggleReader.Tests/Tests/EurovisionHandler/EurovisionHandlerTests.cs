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

        [Theory]
        [InlineData(1970,"Ireland")]
        [InlineData(2010,"Germany")]
        public async Task GetContestEntriesAsync(int year, string expectedWinningCountry)
        {
            var entries = await _eurovisionHandler
                                    .GetContestEntriesAsync(
                                            year: year,
                                            top: null);
            Assert.NotNull(entries);
            Assert.Equal(expectedWinningCountry, entries.First().Country);
        }

        [Fact]
        public async Task GetContestsAsync()
        {
            var contests = await _eurovisionHandler
                                    .GetContestsAsync();
            Assert.NotNull(contests);
            Assert.True(contests.Any());
        }
        [Theory]
        [InlineData(2000,"Denmark")]
        public async Task GetContestAsync(int year, string expectedWinnerCountry)
        {
            var contest = await _eurovisionHandler
                                    .GetContestAsync(year: year);
            Assert.NotNull(contest);
            Assert.Equal(expectedWinnerCountry, contest.Winner.Country);
        }


    }
}
