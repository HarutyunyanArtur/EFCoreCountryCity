using EFCoreContryCity.Models;
using EFCoreCountryCity.Services.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public virtual int Create(T model)
        {
            if (model == null) throw new ArgumentNullException("Model is null");
            _entities.Add(model);
            return _context.SaveChanges();
        }

        public virtual int Delete(T model)
        {
            if (model == null) throw new ArgumentNullException("Model is null");
            _entities.Remove(model);
            return _context.SaveChanges();
        }

        public virtual T Get(int? id)
        {
            return _entities.SingleOrDefault(x=>x.Id==id);
        }

        public virtual  IEnumerable<T> GetAll()
        {
            return _entities.AsEnumerable();
        }

        public virtual int Update(T model)
        {
            if (model == null) throw new ArgumentNullException("Model is null");
            _entities.Update(model);
            return _context.SaveChanges();
        }
    }
}
