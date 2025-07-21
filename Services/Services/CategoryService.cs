using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces.Repositories;
using Service.Abstractions.Dtos.CategoryDto;
using Service.Abstractions.HandlerResponse;
using Service.Abstractions.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository,IMapper mapper)
        {
            _categoryRepository=categoryRepository;
            _mapper=mapper;
        }
        public async Task<ServiceResponse<List<CategoryReadDto>>> GetAllCategories()
        {
            
            var categories = await _categoryRepository.GetAllAsync();
            if(categories is null || !categories.Any())
            {
                throw new NotFoundException("No categories found.");
            }

            var categoryDtos = _mapper.Map<List<CategoryReadDto>>(categories);
            return new ServiceResponse<List<CategoryReadDto>>(categoryDtos);

        }
        public async Task<ServiceResponse<CategoryReadDto>> GetCategoryById(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if(category is null)
            {
                throw new NotFoundException($"Category with ID {id} not found.");
            }
            var categoryDto = _mapper.Map<CategoryReadDto>(category);
            return new ServiceResponse<CategoryReadDto>(categoryDto);


        }
        public async Task<ServiceResponse<CategoryReadDto>> AddCategory(CategoryAddDto categoryAddDto)
        {
            var response = new ServiceResponse<CategoryReadDto>();
            if(categoryAddDto is null)
            {
                response.Message="Invalid data provided.";
                response.Success=false;
                return response;
            }
            try
            {
                var data = _mapper.Map<Category>(categoryAddDto);
                await _categoryRepository.AddAsync(data);
                await _categoryRepository.SaveAsync();

                response.Data =_mapper.Map<CategoryReadDto>(data);
                response.Message="Category added successfully";
            }
            catch(Exception ex)
            {
                response.Success =false;
                var innerMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                response.Message = $"An error occurred while adding the category. Details: {innerMessage}";
            }
            return response;
        }
        public async Task<ServiceResponse<bool>> UpdateCategory(CategoryUpdateDto categoryUpdateDto)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                if(categoryUpdateDto is null)
                {
                    response.Message="Invalid data provided.";
                    response.Success=false;
                    return response;
                }
                var existingItem = await _categoryRepository.GetByIdAsync(categoryUpdateDto.Id);
                if(existingItem is null)
                {
                    response.Success=false;
                    response.Message = $"Category does not exist.";
                    response.Data = false;
                    return response;
                }
                var item = _mapper.Map(categoryUpdateDto, existingItem);
                await _categoryRepository.SaveAsync();
                response.Success = true;
                response.Message = "Category updated successfully.";
            }
            catch(Exception ex)
            {
                response.Success =false;
                var innerMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                response.Message = $"An error occurred while Updating the category. Details: {innerMessage}";
                response.Data = false;
            }
            return response;
        }
        public async Task<ServiceResponse<bool>> DeleteCategory(int id)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var item = await _categoryRepository.GetByIdAsync(id);
                if(item is null)
                {
                    response.Success =false;
                    response.Message = "Category not found";
                    return response;
                }
                _categoryRepository.Delete(item);
               await _categoryRepository.SaveAsync();
                response.Message = "Category deleted successfully";
                response.Data = true;
            }
            catch(Exception ex)
            {
                response.Success =false;
                var innerMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                response.Message = $"An error occurred while deleting the category. Details: {innerMessage}";
                response.Data = false;
            }
            return response;
        }
        public async Task SaveChanges()
        {
            await _categoryRepository.SaveAsync();
        }
    }
}
