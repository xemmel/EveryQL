using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace KaggleReader.Library.Extensions
{
    public static class StreamExtensions
    {
        public static Task<string> AsStringAsync(this Stream stream, Encoding? encoding = null)
        {
            encoding ??= Encoding.UTF8;
            StreamReader sr = new StreamReader(stream, encoding);
            return sr.ReadToEndAsync();
        }
    }
}
