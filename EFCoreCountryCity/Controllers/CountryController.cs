

using EFCoreContryCity.Models;
using EFCoreCountryCity.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace EFCoreCountryCity.Controllers
{
    public class CountryController : Controller
    {
        private readonly IBaseService<Country> _countryService;
        public CountryController(IBaseService<Country> countryService)
        {
            _countryService = countryService;
        }
        // GET: CountryController
        public ActionResult Index(string sortOrder,string searchItem, string currenFilter,int? pageNumber)
        {
            ViewData["NameSort"] = string.IsNullOrEmpty(sortOrder) ? "Name" : "";
            ViewData["CapitalSort"] = sortOrder == "Capital" ? "Capital_Desc" : "Capital";
            ViewData["PresidentSort"] = sortOrder == "President" ? "President_Desc" : "President";
            ViewData["PopulationSort"] = sortOrder == "Population"? "Population_Desc" : "Population";
            ViewData["LanguageSort"] = sortOrder == "Language"? "Language_Desc" : "Language";
            ViewData["CurenetSort"] = currenFilter;
            if (searchItem !=null)
            {
                pageNumber = 1;
            }
            else
            {
                searchItem = currenFilter;
            }

            ViewData["Filter"] = searchItem;

            var country = from c in _countryService.GetAll()
                          select c;
            if (!string.IsNullOrEmpty(searchItem))
            {
                country = country.Where(c => c.Name.Contains(searchItem) || c.Capital.Contains(searchItem) || c.President.Contains(searchItem));
            }
            switch (sortOrder)
            {
                case "Name":
                    country = country.OrderBy(c => c.Name);
                    break;
                case "Capital":
                    country = country.OrderBy(c => c.Capital);
                    break;
                case "Capital_Desc":
                    country = country.OrderByDescending(c => c.Capital);
                    break;
                case "President":
                    country = country.OrderBy(c => c.President);
                    break;
                case "President_Desc":
                    country = country.OrderByDescending(c => c.President);
                    break;
                case "Population":
                    country = country.OrderBy(c => c.Population);
                    break;
                case "Population_Desc":
                    country = country.OrderByDescending(c => c.Population);
                    break;
                case "Language":
                    country = country.OrderBy(c => c.Language);
                    break;
                case "Language_Desc":
                    country = country.OrderByDescending(c => c.Language);
                    break;
                default:
                    country = country.OrderByDescending(c => c.Name);
                    break;
            }
            int pageSize = 3;
            return View(PaginatedList<Country>.Create(country.AsQueryable(), pageNumber ?? 1, pageSize));
        }

        // GET: CountryController/Details/5
        public ActionResult Details(int id)
        {
            return View(_countryService.Get(id));
        }

        // GET: CountryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CountryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Country country)
        {
            try
            {
                _countryService.Create(country);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CountryController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_countryService.Get(id));
        }

        // POST: CountryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Country country)
        {
            try
            {
                _countryService.Update(country);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CountryController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_countryService.Get(id));
        }

        // POST: CountryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Country country)
        {
            try
            {
                _countryService.Delete(country);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
