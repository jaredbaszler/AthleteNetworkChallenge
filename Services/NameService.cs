using AthleteNetworkChallenge.Web.Models.DataModels;
using System.Text.Json;

namespace AthleteNetworkChallenge.Web.Services
{
    public class NameService : INameService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public NameService(IHttpClientFactory httpClientFactory) =>
            _httpClientFactory = httpClientFactory;

        public async Task<IEnumerable<NationalizeName>> GetNationalizeNames(string namesToSearch)
        {
            if (namesToSearch != null && namesToSearch.Trim().Length == 0)
            {
                return new List<NationalizeName>();
            }

            var httpClient = _httpClientFactory.CreateClient("Nationalize");

            var queryString = "?";

            foreach (var name in namesToSearch!.TrimEnd(' ').TrimEnd(',').Split(",", StringSplitOptions.TrimEntries).ToList())
            {
                queryString += queryString.Length == 1 ? "" : "&";
                queryString += $"name[]={name}";
            }

            var response = await httpClient.GetAsync($"{queryString}");

            if (response.IsSuccessStatusCode)
            {
                using var contentStream = await response.Content.ReadAsStreamAsync();
                var results = await JsonSerializer.DeserializeAsync<IEnumerable<NationalizeName>>(contentStream);
                if (results != null)
                {
                    return results;
                }
            }

            return new List<NationalizeName>();
        }
    }
}
