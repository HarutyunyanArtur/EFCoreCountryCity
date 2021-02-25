using EFCoreContryCity.Models;
using System.Collections.Generic;

namespace EFCoreCountryCity.Services
{
    public interface IBaseService<T> where T: BaseEntity
    {
        T Get(int? id);
        IEnumerable<T> GetAll();
        int Create(T model);
        int Update(T model);
        int Delete(T model);
    }
}
