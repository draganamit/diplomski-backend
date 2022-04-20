using System.Collections.Generic;
using System.Threading.Tasks;
using diplomski_backend.Dtos.Categories;
using diplomski_backend.Models;

namespace diplomski_backend.Services.CategoryService
{
    public interface ICategoryService
    {
         public Task<ServiceResponse<List<GetCategoryDto>>> GetAllCategories();
         public Task<ServiceResponse<GetCategoryDto>> GetCategoryById(int id);
         public Task<ServiceResponse<List<GetCategoryDto>>> AddCategory(AddCategoryDto newCategory);
         public Task<ServiceResponse<GetCategoryDto>> UpdateCategory(UpdateCategoryDto updatedCategory);
         public Task<ServiceResponse<List<GetCategoryDto>>> DeleteCategory(int id);
    }
}