using System.Collections.Generic;
using System.Threading.Tasks;
using diplomski_backend.Dtos.Category;
using diplomski_backend.Models;
using diplomski_backend.Services.CategoryService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace diplomski_backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        [AllowAnonymous]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _categoryService.GetAllCategories());
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _categoryService.GetCategoryById(id));
        }

        [AllowAnonymous]
        [HttpPut]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updatedCategory)
        {
            ServiceResponse<GetCategoryDto> response = await _categoryService.UpdateCategory(updatedCategory);
            if(response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> AddCategory(AddCategoryDto newCategory)
        {
            return Ok(await _categoryService.AddCategory(newCategory));
        }

        [AllowAnonymous]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            ServiceResponse<List<GetCategoryDto>> response = await _categoryService.DeleteCategory(id);
            if(response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}