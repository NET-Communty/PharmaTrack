using Service.Abstractions.Dtos.MedicineBatchDto;
using Service.Abstractions.Dtos.MedicineDto;
using Service.Abstractions.HandlerResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstractions.IServices
{
    public interface IMedicineBatchService
    {

        Task<ServiceResponse<List<GetMedicineBatchDto>>> GetAllMedicineBatches();
        Task<ServiceResponse<GetMedicineBatchDto>> GetMedicineBatchById(int id);

        Task<ServiceResponse<List<GetMedicineBatchDto>>> GetBatchesByIdMedicineId(int medicineId);

        Task<ServiceResponse<List<GetMedicineBatchDto>>> GetMedicineBatchesByIdSupplierId(int supplierId);

        //Task<ServiceResponse<List<GetMedicineBatchDto>>> GetExpiredBatches();
        //Task<ServiceResponse<List<GetMedicineBatchDto>>> GetBatchesExpiringSoon(int days);

        Task<ServiceResponse<AddMedicineBatchDto>> AddMedicineBatch(AddMedicineBatchDto medicineAddDto );
        Task<ServiceResponse<bool>> UpdateMedicineBatch(UpdateMedicineBatchDto medicineUpdateDto);
        Task<ServiceResponse<ArchiveBatchDto>> ArchiveMedicineBatch(int id);
        Task SaveChanges();
    }
}
