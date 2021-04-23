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

        public static bool WildContains(this string input, string pattern)
        {
            return input.ToLower().Contains(pattern.ToLower());
        }

        public static bool WildCombiContains(this string input, string patterns)
        {
            if (string.IsNullOrWhiteSpace(patterns))
                return input.WildEquals(patterns);
            var patternArray = patterns.Split("|");
            foreach (var pattern in patternArray)
            {
                var result = input.WildContains(pattern);
                if (result)
                    return true;
            }
            return false;
        }

        public static bool WildEquals(this string input, string compare)
        {
            return (string.Compare(input, compare, ignoreCase: true) == 0);
        }

        public static bool WildCombiEquals(this string input, string compares)
        {
            if (string.IsNullOrWhiteSpace(compares))
                return input.WildEquals(compares);
            var compareArray = compares.Split("|");
            foreach (var compare in compareArray)
            {
                var result = input.WildEquals(compare);
                if (result)
                    return true;
            }
            return false;
        }
    }
}
