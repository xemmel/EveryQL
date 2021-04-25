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
        public EveryGraphQuery(IEurovisionHandler eurovisionHandler, ICountryHandler countryHandler)
        {
            Field<StringGraphType>("version", resolve: _ => $"0.3.1");

            #region countries
            FieldAsync<ListGraphType<CountryGraphType>>(
                    "countries",
                    arguments: (new QueryArguments()).AddFiltersArgument(),
                    resolve: async context => {

                        var filters = context.ResolveArgumentFilters();
                        var countries = await countryHandler.GetCountriesAsync(cancellationToken: context.CancellationToken);
                        if (!string.IsNullOrWhiteSpace(filters))
                            countries = countries.Where(e => e.AnyPropertyCombiContains(filters));
                        return countries;

                    });
            #endregion

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
                        string? filters = context.ResolveArgumentFilters();
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
