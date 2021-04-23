using System.IO;
using System.Text;

namespace KaggleReader.Library.Extensions
{
    public static class StringExtensions
    {
        public static Stream AsStream(this string input, Encoding? encoding = null)
        {
            encoding ??= Encoding.UTF8;
            MemoryStream result = new MemoryStream(encoding.GetBytes(input));
            return result;
        }
    }
}
