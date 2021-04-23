using KaggleReader.Library.Models.Eurovision;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KaggleReader.Library.Extensions
{
    public static class EurovisionExtensions
    {

        public static IEnumerable<EuroVisionContestModel> ReOrder(this IEnumerable<EuroVisionContestModel> contests, EntrySortEnum entrySortEnum)
        {
            foreach(var contest in contests)
                contest.Entries = contest.Entries.ReOrder(entrySortEnum: entrySortEnum).ToArray();
            return contests;
        }
        public static EuroVisionContestModel ReOrder(this EuroVisionContestModel contest, EntrySortEnum entrySortEnum)
        {
            contest.Entries.ReOrder(entrySortEnum: entrySortEnum);
            return contest;
        }

        public static IEnumerable<EuroVisionLyricModel> ReOrder(this IEnumerable<EuroVisionLyricModel> entries, EntrySortEnum entrySortEnum)
        {
            if (entrySortEnum == EntrySortEnum.Entry)
                entries = entries.OrderBy(r => r.EntryNumber);
            else if (entrySortEnum == EntrySortEnum.Placement)
                entries = entries.OrderBy(r => r.Placement);
            return entries;
        }
    }
}
