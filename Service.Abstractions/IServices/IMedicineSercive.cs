using Service.Abstractions.Dtos.MedicineDto;
using Service.Abstractions.HandlerResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstractions.IServices
{
    public interface IMedicineSercive
    {
        Task<ServiceResponse<List<MedicineReadDto>>> GetAllMedicine();
        Task<ServiceResponse<MedicineReadDto>> GetMedicineById(int id);
        Task<ServiceResponse<List<MedicineReadDto>>> GetMedicineByIdCategoryId(int categoryId);
        Task<ServiceResponse<List<MedicineReadDto>>> GetMedicineByIdSupplierId(int supplierId);
        Task<ServiceResponse<MedicineReadDto>> AddMedicine(MedicineAddDto medicineAddDto, string webRootPath);
        Task<ServiceResponse<bool>> UpdateMedicine(MedicineUpdateDto medicineUpdateDto, string webRootPath);
        Task<ServiceResponse<bool>> DeleteMedidne(int id);
        Task SaveChanges();
    }
}
