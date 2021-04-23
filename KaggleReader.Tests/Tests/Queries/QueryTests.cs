using EveryGraph.DI;
using EveryGraph.GraphQL.Base;
using GraphQL.Server;
using GraphQL.SystemTextJson;
using KaggleReader.Library.DI;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;

namespace KaggleReader.Tests.Tests.Queries
{
    public class QueryTests
    {
        private readonly EveryGraphSchema _schema;

        public QueryTests()
        {
            var services = new ServiceCollection();
            services.AddKaggleReader()
                    .AddEveryGraph();

           
            _schema = services.BuildServiceProvider().GetRequiredService<EveryGraphSchema>();
        }

        [Theory]
        [InlineData("{ version }")]
        [InlineData("{ entries { year } }")]
        [InlineData("{ entries(filters : \"logan\") { year } }")]
        [InlineData("{ entries(country : \"Germany\") { year } }")]
        [InlineData("{ entries(winner : \"Germany\") { year } }")]
        [InlineData("{ entries(winner : \"Germany\") { year contest(top: 3) { song country } } }")]
        [InlineData("{ entries(host : \"Germany\") { year contest(top: 3) { song country } } }")]





        [InlineData("{ contest(year: 1980) { year } }")]

        public async Task QueryGraphQL(string query)
        {
            var result = await _schema.ExecuteAsync(_ => {
                _.Query = query;
            });
            Assert.NotNull(result);
            Assert.DoesNotContain("\"errors\"", result);
        }
    }
}
