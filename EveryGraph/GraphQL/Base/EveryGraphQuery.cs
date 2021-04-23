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
            Field<StringGraphType>("version", resolve: _ => $"0.3.0");



            #region entries
            FieldAsync<ListGraphType<EurovisionLyricGraphType>>(
                    "entries",
                    arguments: (new QueryArguments())
                                    .AddFiltersArgument()
                                    .AddCountryArgument()
                                    .AddWinnerArgument()
                                    .AddHostArgument(),
                    resolve: async context =>
                    {
                        var entries = await eurovisionHandler
                                    .GetLyricsAsync(
                                            cancellationToken: context.CancellationToken);
                        string? filters = context.GetArgument<string>("filters");
                        string? country = context.GetArgument<string>("country");
                        string? winner = context.GetArgument<string>("winner");
                        string? host = context.GetArgument<string>("host");

                        if (!string.IsNullOrWhiteSpace(host))
                            return entries
                                        .Where(e => (e.HostCountry.WildEquals(host) && (e.Placement == 1)))
                                        .OrderBy(e => e.Year);

                        if (!string.IsNullOrWhiteSpace(winner))
                            return entries
                                        .Where(e => (e.Country.WildEquals(winner) && (e.Placement == 1)))
                                        .OrderBy(e => e.Year);

                        if (!string.IsNullOrWhiteSpace(country))
                            return entries
                                        .Where(e => e.Country.WildEquals(country))
                                        .OrderBy(e => e.Year);
                        if (string.IsNullOrWhiteSpace(filters))
                            return entries;
                        return entries.Where(e => e.AnyPropertyCombiContains(filters));
                    });
            #endregion

            #region Contest
            Field<EuroVisionContestGraphType>(
                "contest",
                arguments: (new QueryArguments())
                                .AddYearArgument(),

                resolve: context =>
                {
                    var year = context.GetArgument<int>("year");

                    return eurovisionHandler
                                .GetContestAsync(
                                        year: year,
                                        entrySortEnum: null,
                                        cancellationToken: context.CancellationToken);
                });
            #endregion


            #region Contests
            Field<ListGraphType<EuroVisionContestGraphType>>(
                    "contests",
                    arguments: (new QueryArguments()),
                    resolve: context =>
                    {
                        return eurovisionHandler
                                    .GetContestsAsync(cancellationToken: context.CancellationToken);
                    });
            #endregion
        }
    }
}
