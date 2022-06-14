using AthleteNetworkChallenge.Web.Models;
using AthleteNetworkChallenge.Web.Models.DataModels;
using AthleteNetworkChallenge.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AthleteNetworkChallenge.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly INameService _nameService;

        public HomeController(INameService nameService)
        {
            _nameService = nameService ?? throw new ArgumentNullException(nameof(nameService));
        }

        public IActionResult Index()
        {
            var vm = new IndexViewModel
            {
                Names = new List<NationalizeName>()
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string namesToSearch, bool filterNames)
        {
            var vm = new IndexViewModel();
            vm.Names = new List<NationalizeName>();

            // Now check list of names is less than 10
            var listOfNames = namesToSearch?.Split(",").ToList();

            if ((listOfNames?.Count > 10) || namesToSearch == null)
            {
                ModelState.AddModelError("NamesToSearch", "Names must be non-blank and max of 10 names");
                return View(vm);
            }

            var names = (await _nameService.GetNationalizeNames(namesToSearch)).ToList();

            if (filterNames)
            {
                var maxProb = names.SelectMany(a => a.Countries).OrderByDescending(a => a.Probability).FirstOrDefault();
                var minProb = names.SelectMany(a => a.Countries).OrderBy(a => a.Probability).FirstOrDefault();

                // There could potentially be ties here resulting in inconsistent results
                var maxName = names.Where(a => a.Countries?.Any(
                    b => b.CountryId == maxProb?.CountryId && b.Probability == maxProb?.Probability) ?? false).FirstOrDefault();

                var minName = names.Where(a => a.Countries?.Any(
                    b => b.CountryId == minProb?.CountryId && b.Probability == minProb?.Probability) ?? false).FirstOrDefault();

                names = new List<NationalizeName>();

                if (maxName != null)
                    names.Add(maxName);

                if (minName != null)
                    names.Add(minName);
            }

            vm.Names = names;

            return View(vm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}