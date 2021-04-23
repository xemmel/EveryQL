using KaggleReader.Library.Models.Eurovision;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace KaggleReader.Library.Services
{
    public interface IEurovisionHandler
    {
        Task<EuroVisionContestModel?> GetContestAsync(int year, EntrySortEnum? entrySortEnum = null, CancellationToken cancellationToken = default);
        Task<IEnumerable<EuroVisionLyricModel>> GetContestEntriesAsync(int year, int? top = null, EntrySortEnum? entrySortEnum = null, CancellationToken cancellationToken = default);
        Task<IEnumerable<EuroVisionContestModel>> GetContestsAsync(EntrySortEnum? entrySortEnum = null, int? top = null, CancellationToken cancellationToken = default);
        Task<IEnumerable<EuroVisionLyricModel>> GetLyricsAsync(CancellationToken cancellationToken = default);
    }
}