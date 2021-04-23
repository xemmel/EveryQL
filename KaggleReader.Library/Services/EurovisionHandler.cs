using KaggleReader.Library.Models.Eurovision;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace KaggleReader.Library.Services
{
    public class EurovisionHandler : IEurovisionHandler
    {
        private readonly IJsonHandler _jsonHandler;
        private IEnumerable<EuroVisionLyricModel> _cache = null;

        public EurovisionHandler(IJsonHandler jsonHandler)
        {
            _jsonHandler = jsonHandler;
        }

        public async Task<IEnumerable<EuroVisionLyricModel>> GetLyricsAsync(CancellationToken cancellationToken = default)
        {
            if (_cache == null) {
                string assemblyFile =
                   Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                string path = Path.Combine(assemblyFile, "Files", "eurovision-lyrics.json");
                var stream = File.OpenRead(path: path);
                _cache = await _jsonHandler.DeserializeKaggleJsonAsync<EuroVisionLyricModel>(jsonStream: stream, cancellationToken: cancellationToken);
            }
            return _cache;
        }
    }
}
