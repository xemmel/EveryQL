using KaggleReader.Library.Models.Eurovision;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace KaggleReader.Library.Services
{
    public interface IEurovisionHandler
    {
        Task<IEnumerable<EuroVisionLyricModel>> GetLyricsAsync(CancellationToken cancellationToken = default);
    }
}