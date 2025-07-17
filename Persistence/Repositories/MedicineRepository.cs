using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Service.Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class MedicineRepository : GenericRepository<Medicine>, IMedicineRepository
    {
       public MedicineRepository(PharmaProjectContext context) : base(context) { }

        public async Task<IEnumerable<Medicine>> GetAllMedicineByCategoryId(int categoryId)
        {
            var category = await _context.categories.FindAsync(categoryId);
            if(category!=null)
            {
                return await _context.medicines.Where(m => m.CategoryId==categoryId).AsNoTracking().ToListAsync();
            }
            return Enumerable.Empty<Medicine>();
        }
        public async Task<IEnumerable<Medicine>> GetAllMedicineBySupplierId(int SupplierId)
        {
            var supplier = await _context.suppliers.FindAsync(SupplierId);
            if(supplier!=null)
            {
                return await _context.medicines.Where(s => s.SupplierId==SupplierId).AsNoTracking().ToListAsync();
            }
            return Enumerable.Empty<Medicine>();
        }
    }
}
