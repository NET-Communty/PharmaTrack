using Service.Abstractions.Dtos.CategoryDto;
using Service.Abstractions.Dtos.SupplierDto;
using Service.Abstractions.HandlerResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstractions.IServices
{
    public interface ISupplierService
    {
        Task<ServiceResponse<List<SupplierReadDto>>> GetAllSuppliers();
        Task<ServiceResponse<SupplierReadDto>> GetSupplierById(int id);
        Task<ServiceResponse<SupplierReadDto>> AddSupplier(SupplierAddDto supplierAddDto);
        Task<ServiceResponse<bool>> UpdateSupplier(SupplierUpdateDto supplierUpdateDto);
        Task<ServiceResponse<bool>> DeleteSupplier(int id);
        Task SaveChanges();
    }
}
