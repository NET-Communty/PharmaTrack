using Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task SaveAsync();
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;

    }
}
