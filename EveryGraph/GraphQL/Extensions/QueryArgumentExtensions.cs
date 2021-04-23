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
        public static QueryArguments AddCountryArgument(this QueryArguments queryArguments)
        {
            queryArguments.Add(new QueryArgument<StringGraphType> { Name = "country", Description = "Entry Country" });
            return queryArguments;
        }

        public static QueryArguments AddWinnerArgument(this QueryArguments queryArguments)
        {
            queryArguments.Add(new QueryArgument<StringGraphType> { Name = "winner", Description = "Entry Country" });
            return queryArguments;
        }

        public static QueryArguments AddHostArgument(this QueryArguments queryArguments)
        {
            queryArguments.Add(new QueryArgument<StringGraphType> { Name = "host", Description = "Host Country" });
            return queryArguments;
        }

        public static QueryArguments AddTopArgument(this QueryArguments queryArguments)
        {
            queryArguments.Add(new QueryArgument<IntGraphType> { Name = "top", Description = "Specify the result count max" });
            return queryArguments;
        }
    }
}
