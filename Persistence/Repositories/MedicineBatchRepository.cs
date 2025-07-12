using Domain.Entities;
using Persistence.Context;
using Service.Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class MedicineBatchRepository : GenericRepository<MedicineBatch>, IMedicineBatchRepository
    {
        public MedicineBatchRepository(PharmaProjectContext context) : base(context) { }
    }

}
