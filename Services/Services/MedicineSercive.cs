using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
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
    public class MedicineSercive : IMedicineSercive
    {
        private readonly IMedicineRepository _medicineRepository;
        private readonly IMapper _mapper;

        public MedicineSercive(IMedicineRepository medicineRepository,IMapper mapper)
        {
            _medicineRepository=medicineRepository;
            _mapper=mapper;
        }
        public async Task<ServiceResponse<List<MedicineReadDto>>> GetAllMedicine()
        {
            var response = new ServiceResponse<List<MedicineReadDto>>();
            var medicine= await _medicineRepository.GetAllAsync();
            if (medicine?.Any()!= true)
            {
                response.Message="No medicine found";
                response.Success=false;
                return response;
            }
            response.Data=_mapper.Map<List<MedicineReadDto>>(medicine);
            response.Message="Medicines retrieved successfully";
            return response;
        }
        public async Task<ServiceResponse<MedicineReadDto>> GetMedicineById(int id)
        {
            var response = new ServiceResponse<MedicineReadDto>();
            var medicine = await _medicineRepository.GetByIdAsync(id);
            if (medicine is null)
            {
                response.Message="Medicine does not exist";
                response.Success=false;
                return response;
            }
            response.Data=_mapper.Map<MedicineReadDto>(medicine);
            response.Message="Medicine retrieved successfully";
            return response;
        }
        public async Task<ServiceResponse<List<MedicineReadDto>>> GetMedicineByIdCategoryId(int categoryId)
        {
            var response = new ServiceResponse<List<MedicineReadDto>>();
            var medicine = await _medicineRepository.GetAllMedicineByCategoryId(categoryId);
            if (medicine?.Any()!= true)
            {
                response.Message="No medicine found";
                response.Success=false;
                return response;
            }
            response.Data=_mapper.Map<List<MedicineReadDto>>(medicine);
            response.Message="Medicines retrieved successfully";
            return response;
        }
        public async Task<ServiceResponse<List<MedicineReadDto>>> GetMedicineByIdSupplierId(int supplierId)
        {
            var response = new ServiceResponse<List<MedicineReadDto>>();
            var medicine = await _medicineRepository.GetAllMedicineBySupplierId(supplierId);
            if (medicine?.Any()!= true)
            {
                response.Message="No medicine found";
                response.Success=false;
                return response;
            }
            response.Data=_mapper.Map<List<MedicineReadDto>>(medicine);
            response.Message="Medicines retrieved successfully";
            return response;
        }
        public async Task<ServiceResponse<MedicineReadDto>> AddMedicine(MedicineAddDto medicineAddDto, string webRootPath)
        {
            var response = new ServiceResponse<MedicineReadDto>();
            if(medicineAddDto is null)
            {
                response.Message="Invalid Medicine data provided";
                response.Success=false;
                return response;
            }
            try
            {
                string imagePath = null;
                if(medicineAddDto.Image!=null&&medicineAddDto.Image.Length>0)
                {
                    imagePath=await SaveImageAsync(medicineAddDto.Image, webRootPath);
                }
                var medicine = _mapper.Map<Medicine>(medicineAddDto);
                medicine.ImageUrl=imagePath;
                await _medicineRepository.AddAsync(medicine);
                await _medicineRepository.SaveAsync();
                response.Data = _mapper.Map<MedicineReadDto>(medicine);
                response.Success=true;
                response.Message="Medicine added successfully.";
            }
            catch(Exception ex)
            {
                response.Success =false;
                var innerMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                response.Message = $"An error occurred while Adding the Medicine. Details: {innerMessage}";
            }
            return response;
        }
        public async Task<ServiceResponse<bool>> DeleteMedidne(int id)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var item = await _medicineRepository.GetByIdAsync(id);
                if (item is null)
                {
                    response.Success =false;
                    response.Message = "Medicine not found";
                    return response;
                }
                _medicineRepository.Delete(item);
                await _medicineRepository.SaveAsync();
                response.Message = "Medicine deleted successfully";
                response.Data = true;
            }
            catch (Exception ex)
            {
                response.Success =false;
                var innerMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                response.Message = $"An error occurred while deleting the Medicine. Details: {innerMessage}";
                response.Data = false;
            }
            return response;
        }
        public async Task<ServiceResponse<bool>> UpdateMedicine(MedicineUpdateDto medicineUpdateDto, string webRootPath)
        {
            var response = new ServiceResponse<bool>();
            string imagePath = null;
            if (medicineUpdateDto.ImageUrl!=null&& medicineUpdateDto.ImageUrl.Length>0)
            {
                imagePath= await SaveImageAsync(medicineUpdateDto.ImageUrl, webRootPath);
            }
            try
            {
                if (medicineUpdateDto is null)
                {
                    response.Message="Invalid Medicine data provided";
                    response.Success=false;
                    return response;

                }
                var existMedicine = await _medicineRepository.GetByIdAsync(medicineUpdateDto.Id);
                if (existMedicine == null)
                {
                    response.Message="Medicine does not exist .";
                    response.Success=false;
                    return response;
                }
                var medicine = _mapper.Map(medicineUpdateDto,existMedicine);
                medicine.ImageUrl=imagePath;
                await _medicineRepository.SaveAsync();
                response.Message = "Medicine updated successfully.";
                response.Data = true;
            }
            catch (Exception ex)
            {
                response.Success =false;
                var innerMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                response.Message = $"An error occurred while Update the Medicine. Details: {innerMessage}";
                response.Data = false;
            }
            return response;
        }
        public async Task SaveChanges()
        {
            await _medicineRepository.SaveAsync();
        }
        private async Task<string> SaveImageAsync(IFormFile image, string webRootPath)
        {
            string uploadsFolder = Path.Combine(webRootPath, "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            string uniqueFileNme = Guid.NewGuid().ToString()+"_"+Path.GetFileName(image.FileName);
            string filePath = Path.Combine(uploadsFolder, uniqueFileNme);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }
            return Path.Combine("uploads", uniqueFileNme);
        }
    }
}
