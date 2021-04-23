using GraphQL;
using KaggleReader.Library.Models.Eurovision;

namespace EveryGraph.GraphQL.Extensions
{
    public static class ResolveFieldContextExtensions
    {
        public static int? ResolveArgumentTop(this IResolveFieldContext context)
        {
            int? result = context.GetArgument<int>("top");
            return result;
        }

        public static EntrySortEnum ResolveEntrySort(this IResolveFieldContext context)
        {
            var result = context.GetArgument<EntrySortEnum>("order");
            return result;
        }
    }
}
