using EveryGraph.GraphQL.Types;
using GraphQL;
using GraphQL.Types;
using KaggleReader.Library.Services;
using System.Linq;

namespace EveryGraph.GraphQL.Base
{
    public class EveryGraphQuery : ObjectGraphType
    {
        public EveryGraphQuery(IEurovisionHandler eurovisionHandler)
        {
            Field<StringGraphType>("version", resolve: _ => $"0.0.1");



            #region Eurovision Songs
            Field<ListGraphType<EurovisionLyricGraphType>>(
                    "eurovisionSongs",
                    arguments: new QueryArguments { },
                    resolve: context => {
                        return eurovisionHandler
                                    .GetLyricsAsync(
                                            cancellationToken: context.CancellationToken);
                    });
            #endregion

            #region Contest
            FieldAsync<ListGraphType<EurovisionLyricGraphType>>(
                "contest",
                arguments: new QueryArguments { new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "year" } },
                resolve: async context => {
                    var entries = await eurovisionHandler
                                            .GetLyricsAsync(cancellationToken: context.CancellationToken);
                    var year = context.GetArgument<string>("year");
                    var result = entries.Where(e => e.Year.Equals(year)).OrderBy(e => e.Placement);
                    return result;
                });
            #endregion
        }
    }
}
