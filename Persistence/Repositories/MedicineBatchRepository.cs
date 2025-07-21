using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Service.Abstractions.Dtos.MedicineBatchDto;
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
        private readonly PharmaProjectContext _context;
        public MedicineBatchRepository(PharmaProjectContext context) : base(context) {
          _context = context;
        }

        public async Task<List<GetMedicineBatchDto>> GetBatchDetailsAsync()
        {
            var result = await _context.medicineBatches
     .Where(b => !b.IsDeleted)
    .Select(b => new GetMedicineBatchDto
    {
        BatchNumber = b.BatchNumber,
        Quantity = b.Quantity,
        ReceviedAt = b.ReceviedAt,
        ExpirationDate = b.ExpirationDate,
        MedicineName = b.Medicine.Name,
        SupplierName = b.Supplier.Name
    })
    .ToListAsync();



            return result;
        }
    }

}
