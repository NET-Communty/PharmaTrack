﻿using Microsoft.AspNetCore.Authorization;
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
            return Ok(response);

        }
        [HttpGet("GetCategoryById/{id}")]
        public async Task<ActionResult<ServiceResponse<CategoryReadDto>>> GetCategoryById(int id)
        {
            var response = await _categoryService.GetCategoryById(id);
           return Ok(response);
        }
        [HttpPost("CreateCategory")]
        public async Task<ActionResult<ServiceResponse<CategoryReadDto>>> AddCategory(CategoryAddDto categoryAddDto)
        {
            var response = await _categoryService.AddCategory(categoryAddDto);
            return CreatedAtAction(nameof(GetCategoryById), new { id = response.Data.Id },response);
        }
        [HttpDelete("DeleteCategory/{id}")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteCategory(int id)
        {
           var response= await _categoryService.DeleteCategory(id);
           
            return Ok(response);

        }
        [HttpPut("UpdateCategory/{id}")]
        public async Task<ActionResult<ServiceResponse<bool>>> UpdateCategory(int id, CategoryUpdateDto categoryUpdateDto)
        {
            var response = await _categoryService.UpdateCategory(id, categoryUpdateDto);
            return Ok(response);
        }
    }
}
