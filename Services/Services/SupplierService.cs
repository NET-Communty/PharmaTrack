using AutoMapper;
using Domain.Entities;
using Service.Abstractions.Dtos.SupplierDto;
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
    public class SupplierService :ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IMapper _mapper;

        public SupplierService(ISupplierRepository supplierRepository, IMapper mapper)
        {
            _supplierRepository = supplierRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<SupplierReadDto>>> GetAllSuppliers()
        {
            var response = new ServiceResponse<List<SupplierReadDto>>();
            var suppliers = await _supplierRepository.GetAllAsync();
            if (suppliers?.Any() != true)
            {
                response.Message = "No Supplier Found.";
                response.Success = false;
                return response;
            }
            response.Data = _mapper.Map<List<SupplierReadDto>>(suppliers);
            response.Message = "Suppliers retrieved successfully";
            return response;
        }

        public async Task<ServiceResponse<SupplierReadDto>> GetSupplierById(int id)
        {
            var response = new ServiceResponse<SupplierReadDto>();
            var supplier = await _supplierRepository.GetByIdAsync(id);
            if (supplier is null)
            {
                response.Message = "Supplier does not exist";
                response.Success = false;
                return response;
            }
            response.Data = _mapper.Map<SupplierReadDto>(supplier);
            response.Message = "Supplier retrieved successfully";
            return response;
        }

        public async Task<ServiceResponse<SupplierReadDto>> AddSupplier(SupplierAddDto supplierAddDto)
        {
            var response = new ServiceResponse<SupplierReadDto>();
            if (supplierAddDto is null)
            {
                response.Message = "Invalid data provided.";
                response.Success = false;
                return response;
            }
            try
            {
                var supplier = _mapper.Map<Supplier>(supplierAddDto);
                await _supplierRepository.AddAsync(supplier);
                await _supplierRepository.SaveAsync();

                response.Data = _mapper.Map<SupplierReadDto>(supplier);
                response.Message = "Supplier added successfully";
            }
            catch (Exception ex)
            {
                response.Success = false;
                var innerMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                response.Message = $"An error occurred while adding the supplier. Details: {innerMessage}";
            }
            return response;
        }

        public async Task<ServiceResponse<bool>> UpdateSupplier(SupplierUpdateDto supplierUpdateDto)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                if (supplierUpdateDto is null)
                {
                    response.Message = "Invalid data provided.";
                    response.Success = false;
                    return response;
                }
                var existingSupplier = await _supplierRepository.GetByIdAsync(supplierUpdateDto.Id);
                if (existingSupplier is null)
                {
                    response.Success = false;
                    response.Message = $"Supplier does not exist.";
                    return response;
                }
                _mapper.Map(supplierUpdateDto, existingSupplier);
                await _supplierRepository.SaveAsync();
                response.Success = true;
                response.Message = "Supplier updated successfully.";
                response.Data = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred while updating the supplier. Details: {ex.Message}";
                response.Data = false;
            }
            return response;
        }

        public async Task<ServiceResponse<bool>> DeleteSupplier(int id)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var supplier = await _supplierRepository.GetByIdAsync(id);
                if (supplier is null)
                {
                    response.Success = false;
                    response.Message = "Supplier not found";
                    return response;
                }
                _supplierRepository.Delete(supplier);
                await _supplierRepository.SaveAsync();
                response.Message = "Supplier deleted successfully";
                response.Data = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred while deleting the supplier. Details: {ex.Message}";
                response.Data = false;
            }
            return response;
        }

        public async Task SaveChanges()
        {
            await _supplierRepository.SaveAsync();
        }

    }
}
