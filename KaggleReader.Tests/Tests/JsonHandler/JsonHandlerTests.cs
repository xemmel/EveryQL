using KaggleReader.Library.DI;
using KaggleReader.Library.Models.Eurovision;
using KaggleReader.Library.Services;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace KaggleReader.Tests.Tests.JsonHandler
{
    public class JsonHandlerTests
    {
        private readonly IJsonHandler _jsonHandler;

        public JsonHandlerTests()
        {
            _jsonHandler = ServiceFactory.GetService<IJsonHandler>();
        }

        [Fact]
        public async Task GetEuroVisionLyrics()
        {
            string json = await File.ReadAllTextAsync(path: @"C:\temp\Kaggle\eurovision-lyrics.json");
            var lyrics = await _jsonHandler.DeserializeKaggleJsonAsync<EuroVisionLyricModel>(json: json);
            Assert.NotNull(lyrics);
            Assert.True(lyrics.Any());
        }
    }
}
