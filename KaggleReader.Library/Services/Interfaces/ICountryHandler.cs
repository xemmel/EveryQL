using KaggleReader.Library.Models.Countries;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace KaggleReader.Library.Services
{
    public interface ICountryHandler
    {
        ValueTask<IEnumerable<CountryModel>?> GetCountriesAsync(CancellationToken cancellationToken = default);
    }
}