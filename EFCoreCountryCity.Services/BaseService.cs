using EFCoreContryCity.Models;
using EFCoreCountryCity.Services.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EFCoreCountryCity.Services
{
    internal class BaseService<T> : IBaseService<T> where T : BaseEntity
    {
        protected readonly EFCoreCountryCityContext _context;
        private readonly DbSet<T> _entities;
        public BaseService(EFCoreCountryCityContext context)
        {
            _context = context;
            _entities = _context.Set<T>();
        }

        public async Task<int> CreateAsync(T model)
        {
            if (model == null) throw new ArgumentNullException("Model is null");
            await _entities.AddAsync(model);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(T model)
        {
            if (model == null) throw new ArgumentNullException("Model is null");
            _entities.Remove(model);
            return await _context.SaveChangesAsync();
        }

        public virtual async Task<T> GetAsync(int? id)
        {
            return await _entities.SingleOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _entities.ToListAsync();
        }

        public async Task<int> UpdateAsync(T model)
        {
            if (model == null) throw new ArgumentNullException("Model is null");
            _entities.Update(model);
            return await _context.SaveChangesAsync();
        }
    }
}
