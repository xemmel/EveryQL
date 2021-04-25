using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace KaggleReader.Library.Models.Countries
{
    public class CountryModel
    {
        public string Name { get; set; }
        public string Capital { get; set; }
        public int? Population { get; set; }
        public decimal? Area { get; set; }
        public ICollection<string>? CallingCodes { get; set; }
        public ICollection<string>? AltSpellings { get; set; }
        [JsonPropertyName("topLevelDomain")]
        public ICollection<string>? Domains { get; set; }
        [JsonPropertyName("alpha2Code")]
        public string ShortCountryCode { get; set; }
        [JsonPropertyName("alpha3Code")]
        public string CountryCode { get; set; }
        public string Region { get; set; }
        public string SubRegion { get; set; }
        [JsonPropertyName("latlng")]
        public ICollection<decimal> Position { get; set; }
        public string? Demonym { get; set; }
        public decimal? Gini { get; set; }

        public ICollection<string>? TimeZones { get; set; }
        public ICollection<string>? Borders { get; set; }
        public string? NativeName { get; set; }
        [JsonPropertyName("numericCode")]
        public string? NumericCountryCode { get; set; }
        public string? Flag { get; set; }



    }
}
