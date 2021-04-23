using System.Collections.Generic;
using System.Linq;

namespace KaggleReader.Library.Extensions
{
    public static class CollectionExtensions
    {
        public static IEnumerable<T> TakePositive<T>(this IEnumerable<T> collection, int? top)
        {
            if ((top == null) || (top.Value == 0))
                return collection;
            return collection.Take(top.Value);
        }
    }
}
