using System.Collections.Generic;
using System.Linq;

namespace KaggleReader.Library.Models.Eurovision
{
    public class EuroVisionContestModel
    {

        public int Year => Entries.First().Year;
        public string HostCountry => Entries.First().HostCountry;
        public string HostCity => Entries.First().HostCity;
        public EuroVisionLyricModel? Winner => Entries.FirstOrDefault(e => e.Placement == 1);
        public ICollection<EuroVisionLyricModel> Entries { get; set; }
    }
}
