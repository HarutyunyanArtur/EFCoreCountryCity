using EFCoreContryCity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EFCoreCountryCity.Services
{
    public interface IBaseService<T> where T : BaseEntity
    {
        Task<T> GetAsync(int? id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<int> CreateAsync(T model);
        Task<int> UpdateAsync(T model);
        Task<int> DeleteAsync(T model);
    }
}
