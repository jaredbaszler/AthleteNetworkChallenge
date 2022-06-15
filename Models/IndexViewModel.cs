using AthleteNetworkChallenge.Web.Models.DataModels;

namespace AthleteNetworkChallenge.Web.Models
{
    public class IndexViewModel
    {
        private List<NationalizeName> names = new List<NationalizeName>();
        public List<NationalizeName> Names { get => names; set => names = value; }
        public ISO3166.Country[] isoCountyList = ISO3166.Country.List;

        public string? NamesToSearch { get; set; }
        public bool FilterNames { get; set; }
        public string? GetCountryFullName(string? countryTwoLetterAbbreviation)
        {
            return isoCountyList.FirstOrDefault(a => a.TwoLetterCode == countryTwoLetterAbbreviation)?.Name;
        }
    }
}
