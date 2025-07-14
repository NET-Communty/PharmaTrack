using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstractions.Repositories
{
    public interface IMedicineRepository: IGenericRepository<Medicine>
    {
    }
}
