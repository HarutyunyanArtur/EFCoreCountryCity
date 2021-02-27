using EFCoreContryCity.Models;
using EFCoreCountryCity.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreCountryCity.Controllers
{
    public class CountryController : Controller
    {
        private readonly ICountryService _countryService;
        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }
        // GET: CountryController
        public async Task<ActionResult> IndexAsync(string sortOrder, string searchItem, string currenFilter, int? pageNumber)
        {
            ViewData["NameSort"] = string.IsNullOrEmpty(sortOrder) ? "Name" : "";
            ViewData["CapitalSort"] = sortOrder == "Capital" ? "Capital_Desc" : "Capital";
            ViewData["PresidentSort"] = sortOrder == "President" ? "President_Desc" : "President";
            ViewData["PopulationSort"] = sortOrder == "Population" ? "Population_Desc" : "Population";
            ViewData["LanguageSort"] = sortOrder == "Language" ? "Language_Desc" : "Language";
            ViewData["CurenetSort"] = currenFilter;
            if (searchItem != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchItem = currenFilter;
            }

            ViewData["Filter"] = searchItem;

            var country = from c in await _countryService.GetAllAsync()
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
        public async Task<ActionResult> DetailsAsync(int id)
        {
            return View(await _countryService.GetAsync(id));
        }

        // GET: CountryController/CreateAsync
        public ActionResult Create()
        {
            return View();
        }

        // POST: CountryController/CreateAsync
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Country country)
        {
            try
            {
                await _countryService.CreateAsync(country);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CountryController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            return View(await _countryService.GetAsync(id));
        }

        // POST: CountryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, Country country)
        {
            try
            {
                await _countryService.UpdateAsync(country);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CountryController/DeleteAsync/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            return View(await _countryService.GetAsync(id));
        }

        // POST: CountryController/DeleteAsync/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int id, Country country)
        {
            try
            {
                await _countryService.DeleteAsync(country);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
