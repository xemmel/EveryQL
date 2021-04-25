using GraphQL.Types;
using KaggleReader.Library.Models.Countries;

namespace EveryGraph.GraphQL.Types
{
    public class CountryGraphType : AutoRegisteringObjectGraphType<CountryModel>
    {
        public CountryGraphType() : base()
        {

        }
    }
}
