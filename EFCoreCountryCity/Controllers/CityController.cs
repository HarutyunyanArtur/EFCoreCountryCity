using EFCoreContryCity.Models;
using EFCoreCountryCity.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace EFCoreCountryCity.Controllers
{
    public class CityController : Controller
    {
        private readonly ICityService _cityService;
        private readonly IBaseService<Country> _countryService;

        public CityController(ICityService cityService, IBaseService<Country> contryService)
        {
            _cityService = cityService;
            _countryService = contryService;
        }

        // GET: CityController
        public ActionResult Index(string sortOrder, string searchItem, string currentFilter, int? pageNumber)
        {
            ViewData["NameSort"] = string.IsNullOrEmpty(sortOrder) ? "Name_Desc" : "";
            ViewData["PopulationSort"] = sortOrder == "Population" ? "Population_Desc" : "Population";
            ViewData["MayorSort"] = sortOrder == "Mayor" ? "Mayor_Desc" : "Mayor";
            ViewData["CountrySort"] = sortOrder == "Country" ? "Country_Desc" : "Country";
            ViewData["CurrentSort"] = sortOrder;
            if (searchItem != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchItem = currentFilter;
            }
            ViewData["CurrentFilter"] = searchItem;

            var cities = from c in _cityService.GetAll()
                         select c;
            if (!string.IsNullOrEmpty(searchItem))
            {
                cities = cities.Where(c => c.Name.Contains(searchItem) || c.Mayor.Contains(searchItem) || c.Country.Name.Contains(searchItem));
            }
            switch (sortOrder)
            {
                case "Name_Desc":
                    cities = cities.OrderByDescending(c => c.Name);
                    break;
                case "Population":
                    cities = cities.OrderBy(c => c.Population);
                    break;
                case "Population_Desc":
                    cities = cities.OrderByDescending(c => c.Population);
                    break;
                case "Mayor":
                    cities = cities.OrderBy(c => c.Mayor);
                    break;
                case "Mayor_Desc":
                    cities = cities.OrderByDescending(c => c.Mayor);
                    break;
                case "Country":
                    cities = cities.OrderBy(c => c.Country.Name);
                    break;
                case "Country_Desc":
                    cities = cities.OrderByDescending(c => c.Country.Name);
                    break;
                default:
                    cities = cities.OrderBy(c => c.Name);
                    break;
            }

            int pageSize = 3;
            return View(PaginatedList<City>.Create(cities.AsQueryable(),pageNumber??1,pageSize));
        }

        // GET: CityController/Details/5
        public ActionResult Details(int id)
        {
            return View(_cityService.Get(id));
        }

        // GET: CityController/Create
        public ActionResult Create()
        {
            ViewData["Countries"] = new SelectList(_countryService.GetAll(), "Id", "Name");
            return View();
        }

        // POST: CityController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(City city)
        {
            try
            {
                _cityService.Create(city);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CityController/Edit/5
        public ActionResult Edit(int id)
        {
            ViewData["Countries"] = new SelectList(_countryService.GetAll(), "Id", "Name");
            return View(_cityService.Get(id));
        }

        // POST: CityController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, City city)
        {
            try
            {
                _cityService.Update(city);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CityController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_cityService.Get(id));
        }

        // POST: CityController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, City city)
        {
            try
            {
                _cityService.Delete(city);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
