using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
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
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService( IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper=mapper;
            _unitOfWork=unitOfWork;
                
        }
        public async Task<ServiceResponse<List<CategoryReadDto>>> GetAllCategories()
        {
            
            var categories = await _unitOfWork.Repository<Category>().GetAllAsync();
            if (categories is null || !categories.Any())
            {
                throw new NotFoundException("No categories found.");
            }

            var categoryDtos = _mapper.Map<List<CategoryReadDto>>(categories);
            return new ServiceResponse<List<CategoryReadDto>>(categoryDtos);

        }
        public async Task<ServiceResponse<CategoryReadDto>> GetCategoryById(int id)
        {
            var category = await _unitOfWork.Repository<Category>().GetByIdAsync(id);
            if (category is null)
            {
                throw new NotFoundException($"Category with ID {id} not found.");
            }
            return new ServiceResponse<CategoryReadDto>(_mapper.Map<CategoryReadDto>(category));


        }
        public async Task<ServiceResponse<CategoryReadDto>> AddCategory(CategoryAddDto categoryAddDto)
        {

            var category = _mapper.Map<Category>(categoryAddDto);

            if (category is null)
            {
                throw new BadRequestException("Invalid category data provided.");
            }

            await _unitOfWork.Repository<Category>().AddAsync(category);
            await _unitOfWork.SaveAsync();

            var categoryReadDto = _mapper.Map<CategoryReadDto>(category);
            return new ServiceResponse<CategoryReadDto>(categoryReadDto)
            {
                Message = "Category added successfully.",
                Success = true
            };


        }
        public async Task<ServiceResponse<bool>> UpdateCategory(int id, CategoryUpdateDto categoryUpdateDto)
        {
            var category = await _unitOfWork.Repository<Category>().GetByIdAsync(id);

            if (category is null)
            {
                throw new NotFoundException($"Category with ID {id} not found.");
            }

            _mapper.Map(categoryUpdateDto, category);

            await _unitOfWork.SaveAsync();

            return new ServiceResponse<bool>(true, "Category updated successfully.");

        }
        public async Task<ServiceResponse<bool>> DeleteCategory(int id)
        {
            var category = await _unitOfWork.Repository<Category>().GetByIdAsync(id);
            if (category is null)
            {
                throw new NotFoundException($"Category with ID {id} not found.");
            }
            _unitOfWork.Repository<Category>().Delete(category);
            await _unitOfWork.SaveAsync();
            return new ServiceResponse<bool>(true, "Category deleted successfully.");
            


        }
       
    }
}
