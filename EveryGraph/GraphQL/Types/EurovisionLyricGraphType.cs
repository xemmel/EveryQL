using EveryGraph.GraphQL.Extensions;
using GraphQL;
using GraphQL.Types;
using KaggleReader.Library.Models.Eurovision;
using KaggleReader.Library.Services;

namespace EveryGraph.GraphQL.Types
{
    public class EurovisionLyricGraphType : AutoRegisteringObjectGraphType<EuroVisionLyricModel>
    {
        public EurovisionLyricGraphType(IEurovisionHandler eurovisionHandler)
                    : base(
                                f => f.ScoreString,
                                f => f.PlacementString,
                                f => f.YearString)
        {

            Field<EuroVisionContestGraphType>(
                    "contest",
                    arguments: (new QueryArguments()),
                    resolve:  context =>
                    {
                        var year = context.Source.Year;
                        return eurovisionHandler
                                    .GetContestAsync(
                                        year: year,
                                        entrySortEnum: null,
                                        cancellationToken: context.CancellationToken);
                    });
        }

    }
}
