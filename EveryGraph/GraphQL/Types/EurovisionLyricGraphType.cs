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

            Field<ListGraphType<EurovisionLyricGraphType>>(
                    "contest", 
                    arguments: (new QueryArguments()).AddTopArgument(),
                    resolve:  context => {
                var year = context.Source.Year;
                        int? top = context.GetArgument<int>("top");
                return eurovisionHandler
                            .GetContestEntriesAsync(
                                year: year, 
                                top: top,
                                cancellationToken: context.CancellationToken);
            });
        }

    }
}
