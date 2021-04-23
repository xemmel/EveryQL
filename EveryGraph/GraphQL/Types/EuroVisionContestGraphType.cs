using EveryGraph.GraphQL.Extensions;
using GraphQL.Types;
using KaggleReader.Library.Models.Eurovision;
using KaggleReader.Library.Extensions;

namespace EveryGraph.GraphQL.Types
{
    public class EuroVisionContestGraphType : AutoRegisteringObjectGraphType<EuroVisionContestModel>
    {
        public EuroVisionContestGraphType() : base(f => f.Entries, f => f.Winner)
        {
            Field<EurovisionLyricGraphType>("winner", resolve: f => f.Source.Winner);
            Field<ListGraphType<EurovisionLyricGraphType>>(
                    "entries", arguments: (new QueryArguments()).AddTopArgument(),
                    resolve: context =>
                    {
                        int? top = context.ResolveArgumentTop();
                        return context.Source.Entries.TakePositive(top);
                    });

        }
    }
}
