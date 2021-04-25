using KaggleReader.Library.Extensions;
using KaggleReader.Library.Models.Eurovision;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace KaggleReader.Library.Services
{
    public class EurovisionHandler : IEurovisionHandler
    {
        private readonly IJsonHandler _jsonHandler;
        private IEnumerable<EuroVisionLyricModel> _entries = null;
        private IEnumerable<EuroVisionContestModel> _contests = null;
        private EntrySortEnum DEFAULT_ENTRY_SORT = EntrySortEnum.Entry;

        public EurovisionHandler(IJsonHandler jsonHandler)
        {
            _jsonHandler = jsonHandler;
        }


        public async Task<IEnumerable<EuroVisionLyricModel>> GetLyricsAsync(CancellationToken cancellationToken = default)
        {
            if (_entries == null)
            {
                string assemblyFile =
                   Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                string path = Path.Combine(assemblyFile, "Files", "eurovision-lyrics.json");
                var stream = File.OpenRead(path: path);
                _entries = await _jsonHandler.DeserializeKaggleJsonAsync<EuroVisionLyricModel>(jsonStream: stream, cancellationToken: cancellationToken);
            }
            return _entries;
        }

        public async Task<IEnumerable<EuroVisionLyricModel>> GetContestEntriesAsync(int year, int? top = null, EntrySortEnum? entrySortEnum = null, CancellationToken cancellationToken = default)
        {
            var entries = await GetLyricsAsync(cancellationToken: cancellationToken);
            entrySortEnum ??= DEFAULT_ENTRY_SORT;
            var result = entries
                        .Where(e => ((e.Year == year) && ((e.Placement != null) || (e.Year == 1956))));
            
            result = result.ReOrder(entrySortEnum: entrySortEnum.Value);

            if (top == null)
                return result;
            return result.Take(top.Value);
        }

        public async Task<IEnumerable<EuroVisionContestModel>> GetContestsAsync(EntrySortEnum? entrySortEnum = null, int? top = null, CancellationToken cancellationToken = default)
        {
            if (_contests == null)
            {
                var entries = await GetLyricsAsync(cancellationToken: cancellationToken);
                var years = entries.Select(e => e.Year).Distinct().OrderBy(e => e);
                List<EuroVisionContestModel> result = new List<EuroVisionContestModel>();
                foreach (int year in years)
                {
                    var yearEntries = await GetContestEntriesAsync(
                                                year: year, 
                                                top: top, 
                                                entrySortEnum: entrySortEnum, 
                                                cancellationToken: cancellationToken);
                    int index = 1;
                    foreach (var yearEntry in yearEntries)
                        yearEntry.EntryNumber = index++;
                    var contest = new EuroVisionContestModel { Entries = yearEntries.ToArray() };
                    result.Add(contest);
                }
                _contests = result;
            }
            entrySortEnum ??= DEFAULT_ENTRY_SORT;
            var orderedResult = _contests.ReOrder(entrySortEnum: entrySortEnum.Value);
            return orderedResult;
        }
    
        public async Task<EuroVisionContestModel?> GetContestAsync(int year, EntrySortEnum? entrySortEnum = null, CancellationToken cancellationToken = default)
        {
            var contests = await GetContestsAsync(entrySortEnum: entrySortEnum, cancellationToken: cancellationToken);
            return contests.FirstOrDefault(c => c.Year == year);
        }
    }
}
