using EveryGraph.GraphQL.Types;
using GraphQL.Types;
using KaggleReader.Library.Services;

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
        }
    }
}
