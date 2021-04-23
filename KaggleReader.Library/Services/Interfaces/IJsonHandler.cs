using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace KaggleReader.Library.Services
{
    public interface IJsonHandler
    {
        Task<IEnumerable<T>> DeserializeKaggleJsonAsync<T>(string json, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> DeserializeKaggleJsonAsync<T>(Stream jsonStream, CancellationToken cancellationToken = default);
    }
}