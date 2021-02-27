using EFCoreContryCity.Models;
using EFCoreCountryCity.Services.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EFCoreCountryCity.Services
{
    internal class CityService : BaseService<City>, ICityService
    {
        public CityService(EFCoreCountryCityContext context) : base(context)
        {
        }
        public override async Task<IEnumerable<City>> GetAllAsync()
        {
            var result = _context.City.Include(c => c.Country);
            return await result.ToListAsync();
        }
        public override async Task<City> GetAsync(int? id)
        {
            var result = _context.City.Include(c => c.Country).SingleOrDefaultAsync(c=>c.Id==id);
            return await result;
        }
    }
}
