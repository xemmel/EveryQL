using KaggleReader.Library.Models.Countries;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace KaggleReader.Library.Services
{
    public class CountryHandler : ICountryHandler
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private IEnumerable<CountryModel>? _countries = null;
        public CountryHandler(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async ValueTask<IEnumerable<CountryModel>?> GetCountriesAsync(CancellationToken cancellationToken = default)
        {
            if (_countries == null)
            {
                var client = _httpClientFactory.CreateClient();
                string url = "https://restcountries.eu/rest/v2/all";
                var countries = await client.GetFromJsonAsync<IEnumerable<CountryModel>>(requestUri: url, cancellationToken: cancellationToken);
                _countries = countries;
            }
            return _countries;
        }
    }
}
