using GraphQL.Types;

namespace EveryGraph.GraphQL.Extensions
{
    public static class QueryArgumentExtensions
    {
        public static QueryArguments AddFiltersArgument(this QueryArguments queryArguments)
        {
            queryArguments.Add(new QueryArgument<StringGraphType> { Name = "filters", Description = "Filter objects {filter1|filter2}" });
            return queryArguments;
        }
    }
}
