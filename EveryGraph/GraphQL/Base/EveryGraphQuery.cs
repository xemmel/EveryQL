using GraphQL.Types;

namespace EveryGraph.GraphQL.Base
{
    public class EveryGraphQuery : ObjectGraphType
    {
        public EveryGraphQuery()
        {
            Field<StringGraphType>("version", resolve: _ => $"0.0.1");
        }
    }
}
