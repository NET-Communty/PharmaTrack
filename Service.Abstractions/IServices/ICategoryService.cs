using Service.Abstractions.Dtos.CategoryDto;
using Service.Abstractions.HandlerResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstractions.IServices
{
    public interface ICategoryService
    {
        Task<ServiceResponse<List<CategoryReadDto>>> GetAllCategories();
        Task<ServiceResponse<CategoryReadDto>> GetCategoryById(int id);
        Task<ServiceResponse<CategoryReadDto>> AddCategory(CategoryAddDto categoryAddDto);
        Task<ServiceResponse<bool>> UpdateCategory(CategoryUpdateDto categoryUpdateDto);
        Task<ServiceResponse<bool>> DeleteCategory(int id);
        Task SaveChanges();
    }
}
