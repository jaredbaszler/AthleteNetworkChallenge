using System.Text.Json;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json.Serialization;

namespace AthleteNetworkChallenge.Web.Models.DataModels
{
    public partial class NationalizeName
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("country")]
        public List<Country>? Countries { get; set; }
    }

    public partial class Country
    {
        private double probability;

        [JsonPropertyName("country_id")]
        public string? CountryId { get; set; }

        [JsonPropertyName("probability")]
        public double Probability { 
            get { 
                return Math.Round(probability * 100, 2); 
            } 
            set => probability = value; 
        }
    }
}
