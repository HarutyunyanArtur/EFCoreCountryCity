using EFCoreContryCity.Models;
using EFCoreCountryCity.Services.Data;

namespace EFCoreCountryCity.Services
{
    internal class CountryService : BaseService<Country>, ICountryService
    {
        public CountryService(EFCoreCountryCityContext context) :base(context)
        {
        }
    }
}
