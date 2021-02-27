using EFCoreContryCity.Models;
using EFCoreCountryCity.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace EFCoreCountryCity.Controllers
{
    public class CityController : Controller
    {
        private readonly ICityService _cityService;
        private readonly ICountryService _countryService;

        public CityController(ICityService cityService, ICountryService contryService)
        {
            _cityService = cityService;
            _countryService = contryService;
        }

        // GET: CityController
        public async Task<ActionResult> IndexAsync(string sortOrder, string searchItem, string currentFilter, int? pageNumber)
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

            var cities = from c in await _cityService.GetAllAsync()
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
            return View(PaginatedList<City>.Create(cities.AsQueryable(), pageNumber ?? 1, pageSize));
        }

        // GET: CityController/Details/5
        public async Task<ActionResult> DetailsAsync(int id)
        {
            return View(await _cityService.GetAsync(id));
        }

        // GET: CityController/CreateAsync
        public async Task<ActionResult> CreateAsync()
        {
            ViewData["Countries"] = new SelectList(await _countryService.GetAllAsync(), "Id", "Name");
            return View();
        }

        // POST: CityController/CreateAsync
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(City city)
        {
            try
            {
                await _cityService.CreateAsync(city);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CityController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            ViewData["Countries"] = new SelectList(await _countryService.GetAllAsync(), "Id", "Name");
            return View(await _cityService.GetAsync(id));
        }

        // POST: CityController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, City city)
        {
            try
            {
                await _cityService.UpdateAsync(city);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CityController/DeleteAsync/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            return View(await _cityService.GetAsync(id));
        }

        // POST: CityController/DeleteAsync/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int id, City city)
        {
            try
            {
                await _cityService.DeleteAsync(city);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
