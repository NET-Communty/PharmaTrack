using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Abstractions.Dtos.CategoryDto;
using Service.Abstractions.HandlerResponse;
using Service.Abstractions.IServices;

namespace PharmaTrack.Controllers
{

    public class CategoryController : APIController
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService=categoryService;
        }
        [HttpGet("GetAllCategories")]
        public async Task<ActionResult<ServiceResponse<CategoryReadDto>>> GetAllCategories()
        {
            var response = await _categoryService.GetAllCategories();
            if(response.Success)
            {
                return Ok(response.Data);
            }
            return BadRequest(response.Message);
        }
        [HttpGet("GetCategoryById/{id}")]
        public async Task<ActionResult<ServiceResponse<CategoryReadDto>>> GetCategoryById(int id)
        {
            var response = await _categoryService.GetCategoryById(id);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);
        }
        [HttpPost("CreateCategory")]
        public async Task<ActionResult<ServiceResponse<CategoryReadDto>>> AddCategory(CategoryAddDto categoryAddDto)
        {
            var response = await _categoryService.AddCategory(categoryAddDto);
            if (response.Success)
            {
                return Ok(response.Data);
            }
            return BadRequest(response.Message);
        }
        [HttpDelete("DeleteCategory/{id}")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteCategory(int id)
        {
            var response = await _categoryService.DeleteCategory(id);
            if (response.Success)
            {
                return Ok(response.Message);
            }
            return BadRequest(response.Message);

        }
        [HttpPut("UpdateCategory")]
        public async Task<ActionResult<ServiceResponse<bool>>> UpdateCategory(CategoryUpdateDto categoryUpdateDto)
        {
            var response = await _categoryService.UpdateCategory(categoryUpdateDto);
            if (response.Success)
            {
                return Ok(response.Message);
            }
            return BadRequest(response.Message);
        }
    }
}
