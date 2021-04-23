using EveryGraph.GraphQL.Extensions;
using EveryGraph.GraphQL.Types;
using GraphQL;
using GraphQL.Types;
using KaggleReader.Library.Extensions;
using KaggleReader.Library.Services;
using System.Linq;

namespace EveryGraph.GraphQL.Base
{
    public class EveryGraphQuery : ObjectGraphType
    {
        public EveryGraphQuery(IEurovisionHandler eurovisionHandler)
        {
            Field<StringGraphType>("version", resolve: _ => $"0.0.1");



            #region entries
            FieldAsync<ListGraphType<EurovisionLyricGraphType>>(
                    "entries",
                    arguments: (new QueryArguments()).AddFiltersArgument(),
                    resolve: async context => {
                        var entries = await  eurovisionHandler
                                    .GetLyricsAsync(
                                            cancellationToken: context.CancellationToken);
                        string filters = context.GetArgument<string>("filters");
                        if (string.IsNullOrWhiteSpace(filters))
                            return entries;
                        return entries.Where(e => e.AnyPropertyCombiContains(filters));
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
                    var result = entries.Where(e => (e.Year.Equals(year) && (e.Score != null))).OrderBy(e => e.Placement);
                    return result;
                });
            #endregion
        }
    }
}
