using KaggleReader.Library.Extensions;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace KaggleReader.Library.Services
{
    public class JsonHandler : IJsonHandler
    {
        public JsonHandler()
        {

        }

        public async Task<IEnumerable<JsonProperty>> GetParentElementsAsync(Stream jsonStream, CancellationToken cancellationToken = default)
        {
            JsonDocument doc = await JsonDocument
                                        .ParseAsync(
                                            utf8Json: jsonStream,
                                            cancellationToken: cancellationToken);
            List<JsonProperty> result = new List<JsonProperty>();

            foreach (var element in doc.RootElement.EnumerateObject())
            {
                result.Add(element);
            }
            return result;
            return null;
        }

        public async Task<IEnumerable<T>> DeserializeKaggleJsonAsync<T>(string json, CancellationToken cancellationToken = default)
        {
            var stream = json.AsStream();
            var result = await DeserializeKaggleJsonAsync<T>(jsonStream: stream, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<IEnumerable<T>> DeserializeKaggleJsonAsync<T>(Stream jsonStream, CancellationToken cancellationToken = default)
        {
            var elements = await GetParentElementsAsync(jsonStream: jsonStream, cancellationToken: cancellationToken);
            List<T> result = new List<T>();
            foreach (var element in elements)
            {
                var name = element.Name;
                string json = element.Value.ToString();
                T item = JsonSerializer.Deserialize<T>(json: json);
                result.Add(item);
            }
            return result;
        }
    }
}
