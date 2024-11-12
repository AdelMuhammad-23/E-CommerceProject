using AutoMapper;
using E_CommerceProject.Core.DTOs;
using E_CommerceProject.Core.Entities;
using E_CommerceProject.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoriesController(ICategoryRepository categoryRepository,
                                    IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet("CategoryList")]
        public async Task<IActionResult> GetCategoryList()
        {
            var categories = await _categoryRepository.GetListAsync();
            if (categories == null)
                return NotFound("No Categories yet");
            return Ok(categories);
        }
        [HttpGet("Get-Category-By{id}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
                return NotFound($"No Category with this ID:{id}");
            return Ok(category);
        }
        [HttpDelete("Delete-Category-By{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
                return NotFound($"No Category with this ID:{id}");
            await _categoryRepository.DeleteAsync(category);
            return Ok("Delete Category is successfully");
        }

        [HttpPost("Add-Category")]
        public async Task<IActionResult> AddCategory([FromForm] string Name)
        {
            var newCategory = new Category { CategoryName = Name };
            var result = await _categoryRepository.AddCategoryAsync(newCategory);
            if (result == "Category already exists")
                return BadRequest("Category already exists");

            return Ok("Add category is successfully");
        }


        [HttpPut("Update-Category")]
        public async Task<IActionResult> UpdateCategory([FromForm] CategoryDTO categoryDto)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryDto.Id);
            if (category == null)
                return NotFound($"No Category with this ID:{categoryDto.Id}");

            category.CategoryName = categoryDto.Name;

            await _categoryRepository.UpdateAsync(category);
            return Ok("Update category is successfully");
        }

    }
}
