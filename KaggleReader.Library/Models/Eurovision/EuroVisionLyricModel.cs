using System.Text.Json.Serialization;

namespace KaggleReader.Library.Models.Eurovision
{
    public class EuroVisionLyricModel
    {
        [JsonPropertyName("#")]
        public string Id { get; set; }
        public string Country { get; set; }
        [JsonPropertyName("#.l")]
        public string EntryNumberCountry { get; set; }

        public string Artist { get; set; }
        public string Song { get; set; }
        public string Language { get; set; }
        [JsonPropertyName("Pl.")]
        public string PlacementString { get; set; }
        [JsonIgnore]
        public int? Placement
        {
            get
            {
                int result;
                var succeeded = int.TryParse(this.PlacementString, out result);
                if (!succeeded)
                    return null;
                return result;
            }
        }
        [JsonPropertyName("Sc.")]
        public string ScoreString { get; set; }
        public int? Score
        {
            get
            {
                int result;
                var succeeded = int.TryParse(this.ScoreString, out result);
                if (!succeeded)
                    return null;
                return result;
            }
        }
        [JsonPropertyName("Eurovision_Number")]
        public int ContestNumber { get; set; }
        [JsonPropertyName("Year")]
        public string YearString { get; set; }
        [JsonIgnore]
        public int Year => int.Parse(YearString);

        [JsonPropertyName("Host_Country")]
        public string HostCountry { get; set; }
        [JsonPropertyName("Host_City")]
        public string HostCity { get; set; }
        public string Lyrics { get; set; }
        [JsonPropertyName("Lyrics translation")]
        public string LyricsTranslation { get; set; }




    }
}
