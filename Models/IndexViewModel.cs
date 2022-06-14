using AthleteNetworkChallenge.Web.Models.DataModels;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AthleteNetworkChallenge.Web.Models
{
    public class IndexViewModel
    {
        private List<NationalizeName> names = new List<NationalizeName>();
        public List<NationalizeName> Names { get => names; set => names = value; }

        public string? NamesToSearch { get; set; }
        public bool FilterNames { get; set; }
    }
}
