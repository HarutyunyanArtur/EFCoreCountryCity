using Microsoft.EntityFrameworkCore;
using EFCoreContryCity.Models;

namespace EFCoreCountryCity.Services.Data
{
    public class EFCoreCountryCityContext : DbContext
    {
        public EFCoreCountryCityContext (DbContextOptions<EFCoreCountryCityContext> options)
            : base(options)
        {
        }

        public DbSet<City> City { get; set; }

        public DbSet<Country> Country { get; set; }
    }
}
