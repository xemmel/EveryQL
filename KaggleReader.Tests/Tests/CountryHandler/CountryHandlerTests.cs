using KaggleReader.Library.DI;
using KaggleReader.Library.Services;
using System.Threading.Tasks;
using Xunit;

namespace KaggleReader.Tests.Tests.CountryHandler
{
    public class CountryHandlerTests
    {
        private readonly ICountryHandler _countryHandler;

        public CountryHandlerTests()
        {
            _countryHandler = ServiceFactory.GetService<ICountryHandler>();
        }

        [Fact]
        public async Task GetCountriesAsync()
        {
            var countries = await _countryHandler.GetCountriesAsync();
            Assert.NotNull(countries);
        }
    }
}
