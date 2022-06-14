using AthleteNetworkChallenge.Web.Models.DataModels;

namespace AthleteNetworkChallenge.Web.Services
{
    public interface INameService
    {
        Task<IEnumerable<NationalizeName>> GetNationalizeNames(string nameToSearch);
    }
}
