using Domain.Entities;
using Service.Abstractions.Dtos.MedicineBatchDto;
using Service.Abstractions.Dtos.MedicineDto;
using Service.Abstractions.HandlerResponse;
using Service.Abstractions.IServices;
using Service.Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class MedicineBatchService : MedicineBatchServiceBase, IMedicineBatchService
    {
        private readonly IMedicineBatchRepository _medicinebatchrepository;
        private readonly IArchivedMedicineBatchRepository _archivedMedicineBatchRepository;

      

        public MedicineBatchService(IMedicineBatchRepository medicineBatchRepository , IArchivedMedicineBatchRepository archivedMedicineBatchRepository)
        {
            this._medicinebatchrepository = medicineBatchRepository;
            this._archivedMedicineBatchRepository = archivedMedicineBatchRepository;
        }
        public async Task<ServiceResponse<AddMedicineBatchDto>> AddMedicineBatch(AddMedicineBatchDto medicineBatchAddDto)
        {

            var response = new ServiceResponse<AddMedicineBatchDto>();

            if(medicineBatchAddDto is null)
            {
                response.Message = "Invalid Medicine Batch Data Provided ";
                response.Success = false;
                return response;
            }
            try
            {

                // map Dto to Medicinebatch
                MedicineBatch medicineBatch = new MedicineBatch
                {
                    BatchNumber=medicineBatchAddDto.BatchNumber,
                    ExpirationDate=medicineBatchAddDto.ExpirationDate,
                    Quantity=medicineBatchAddDto.Quantity,
                    ReceviedAt=medicineBatchAddDto.ReceivedAt,
                    MedicineId=medicineBatchAddDto.MedicineId,
                    SupplierId=medicineBatchAddDto.SupplierId
                };

                await _medicinebatchrepository.AddAsync(medicineBatch);
                await _medicinebatchrepository.SaveAsync();

                response.Success = true;

                response.Data = medicineBatchAddDto;
                response.Message = "Medicine batch Added Successfully ";
            }
            catch(Exception ex)
            {
                response.Success = false;
                var innerMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                response.Message = $"An error occurred while Adding the MedicineBatch. Details: {innerMessage}";
            }

            return response;
        }




        public async Task<ServiceResponse<ArchiveBatchDto>> ArchiveMedicineBatch(int id)
        {

            var response = new ServiceResponse<ArchiveBatchDto>();
            // May be we will need to add archieved dto

            try
            {
                var medicinebatch = await _medicinebatchrepository.GetByIdAsync(id);

                ArchiveBatchDto archiveBatchDto = new ArchiveBatchDto
                {
                    BatchId=medicinebatch.Id,
                    BatchNumber=medicinebatch.BatchNumber,
                    ArchivedAt=DateTime.UtcNow
                };

                ArchivedMedicineBatch archivedMedicineBatch = new ArchivedMedicineBatch
                {
                    Id = medicinebatch.Id,
                    BatchNumber = medicinebatch.BatchNumber,
                    ArchivedAt = DateTime.UtcNow,
                    Quantity=medicinebatch.Quantity,
                    ExpirationDate=medicinebatch.ExpirationDate,
                    SupplierId=medicinebatch.SupplierId,
                    MedicineId=medicinebatch.MedicineId

                };

                await _archivedMedicineBatchRepository.AddAsync(archivedMedicineBatch);
                await _archivedMedicineBatchRepository.SaveAsync();

                response.Success = true;
                response.Data = archiveBatchDto;
                response.Message = "Batch Deleted Successfully ! ";

            }
            catch (Exception ex)
            {
                response.Success = false;
                var innerMessage = ex.InnerException is not null ? ex.InnerException.Message : ex.Message;
                response.Message = response.Message = $"An error occurred while deleting the Medicine. Details: {innerMessage}";


            }
            return response;

        }






        public async Task<ServiceResponse<List<GetMedicineBatchDto>>> GetAllMedicineBatches()
        { 
            var response = new ServiceResponse<List<GetMedicineBatchDto>>();
            try
            {
                var batches = await _medicinebatchrepository.GetAllAsync();
                List<GetMedicineBatchDto> medicineBatchDtos = new List<GetMedicineBatchDto>();

                foreach (var bat in batches)
                {
                    GetMedicineBatchDto medicineBatchDto = new GetMedicineBatchDto
                    {
                        BatchNumber = bat.BatchNumber,
                        Quantity = bat.Quantity,
                        ReceviedAt = bat.ReceviedAt,
                        ExpirationDate = bat.ExpirationDate

                    };

                    medicineBatchDtos.Add(medicineBatchDto);
                }

                response.Success = true;
                response.Data = medicineBatchDtos;
                response.Message = "Medicine batches retrieved successfully !";


            }
            catch (Exception ex)
            {
                response.Success = false;
                var innerMessage = ex.InnerException is not null ? ex.InnerException.Message : ex.Message;
                response.Message = response.Message = $"An error occurred while deleting the Medicine. Details: {innerMessage}";
            }

            return response;
        }




        public Task<ServiceResponse<List<GetMedicineBatchDto>>> GetBatchesByIdMedicineId(int medicineId)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<List<GetMedicineBatchDto>>> GetBatchesExpiringSoon(int days)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<List<GetMedicineBatchDto>>> GetExpiredBatches()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<GetMedicineBatchDto>> GetMedicineBatchById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<List<GetMedicineBatchDto>>> GetMedicineBatchesByIdSupplierId(int supplierId)
        {

        }

        public async Task SaveChanges()
        {
            await _medicinebatchrepository.SaveAsync();
        }

        public Task<ServiceResponse<bool>> UpdateMedicineBatch(UpdateMedicineBatchDto medicineUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
