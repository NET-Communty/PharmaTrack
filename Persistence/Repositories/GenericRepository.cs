using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly PharmaProjectContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(PharmaProjectContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T).GetProperty("IsDeleted") != null)
            {
                return await _dbSet.Where(e => EF.Property<bool>(e, "IsDeleted") == false).ToListAsync();
            }

            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null && typeof(T).GetProperty("IsDeleted") != null)
            {
                var isDeleted = (bool)typeof(T).GetProperty("IsDeleted")!.GetValue(entity)!;
                if (isDeleted) return null;
            }
            return entity;
        }

        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

        public void Update(T entity) => _dbSet.Update(entity);

        public void Delete(T entity)
        {
            var prop = typeof(T).GetProperty("IsDeleted");
            if (prop != null)
            {
                prop.SetValue(entity, true);
                _dbSet.Update(entity);
            }
            else
            {
                _dbSet.Remove(entity);
            }
        }

        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
