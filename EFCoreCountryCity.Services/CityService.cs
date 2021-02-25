using EFCoreContryCity.Models;
using EFCoreCountryCity.Services.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EFCoreCountryCity.Services
{
    internal class CityService : BaseService<City>, ICityService
    {
        public CityService(EFCoreCountryCityContext context) : base(context)
        {
        }
        public override IEnumerable<City> GetAll()
        {
            var result = _context.City.Include(x => x.Country);
            return result;
        }
        public override City Get(int? id)
        {
            var result = _context.City.Include(x => x.Country);
            return result.SingleOrDefault(x => x.Id == id);
        }
    }
}
