using GraphQL.Types;
using KaggleReader.Library.Models.Eurovision;

namespace EveryGraph.GraphQL.Types
{
    public class EurovisionLyricGraphType : AutoRegisteringObjectGraphType<EuroVisionLyricModel>
    {
        public EurovisionLyricGraphType() : base(f => f.ScoreString, f => f.PlacementString)
        {

        }

    }
}
