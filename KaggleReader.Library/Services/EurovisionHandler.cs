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

        public async Task<IEnumerable<EuroVisionLyricModel>> GetContestEntriesAsync(int year, int? top = null, CancellationToken cancellationToken = default)
        {
            var entries = await GetLyricsAsync(cancellationToken: cancellationToken);

            var result = entries.Where(e => ((e.Year == year) && (e.Placement != null))).OrderBy(e => e.Placement);
            if (top == null)
                return result;
            return result.Take(top.Value);
        }

        public async Task<IEnumerable<EuroVisionContestModel>> GetContestsAsync(CancellationToken cancellationToken = default)
        {
            if (_contests == null)
            {
                var entries = await GetLyricsAsync(cancellationToken: cancellationToken);
                var years = entries.Select(e => e.Year).Distinct().OrderBy(e => e);
                List<EuroVisionContestModel> result = new List<EuroVisionContestModel>();
                foreach (int year in years)
                {
                    var yearEntries = entries
                                        .Where(e => e.Year == year)
                                        .OrderBy(e => e.Id)
                                        .ToArray();
                    int index = 1;
                    foreach (var yearEntry in yearEntries)
                        yearEntry.EntryNumber = index++;
                    var contest = new EuroVisionContestModel { Entries = yearEntries };
                    result.Add(contest);
                }
                _contests = result;
            }
            return _contests;
        }
    
        public async Task<EuroVisionContestModel?> GetContestAsync(int year, CancellationToken cancellationToken = default)
        {
            var contests = await GetContestsAsync(cancellationToken: cancellationToken);
            return contests.FirstOrDefault(c => c.Year == year);
        }
    }
}
