using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Persistence.Context;
using Persistence.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PharmaProjectContext _context;
        private Hashtable _repositories;
        public UnitOfWork(PharmaProjectContext context)
        {
            _context = context;
            _repositories = new Hashtable();

        }
       
        public async Task SaveAsync() => await _context.SaveChangesAsync();

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            var type= typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repository = new GenericRepository<TEntity>(_context);
                _repositories.Add(type, repository);

            }
            return _repositories[type] as IGenericRepository<TEntity> ;
        }

    }
}
