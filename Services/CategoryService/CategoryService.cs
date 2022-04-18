using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using diplomski_backend.Data;
using diplomski_backend.Dtos.Category;
using diplomski_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace diplomski_backend.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
       
        public CategoryService(IMapper mapper, DataContext context)
        {
          
            _context = context;
            _mapper = mapper;

        }
        public async Task<ServiceResponse<List<GetCategoryDto>>> AddCategory(AddCategoryDto newCategory)
        {
            ServiceResponse<List<GetCategoryDto>> response = new ServiceResponse<List<GetCategoryDto>>();
            Category category = _mapper.Map<Category>(newCategory);
            await _context.Category.AddAsync(category);
            await _context.SaveChangesAsync();
            List<Category> dbCategories = await _context.Category.ToListAsync();
            response.Data = _mapper.Map<List<GetCategoryDto>>(dbCategories).ToList();
            return response;
        }

        public async Task<ServiceResponse<List<GetCategoryDto>>> DeleteCategory(int id)
        {
            ServiceResponse<List<GetCategoryDto>> response = new ServiceResponse<List<GetCategoryDto>>();
            Category category = await _context.Category.FirstOrDefaultAsync(x => x.Id == id);
            List<Product> products = await _context.Product.Where(x => x.Category.Id == category.Id).ToListAsync();
            if(products.Count == 0)
            {
                 _context.Category.Remove(category);
                 await _context.SaveChangesAsync();

                List<Category> dbCategories = await _context.Category.ToListAsync();
                 response.Data = _mapper.Map<List<GetCategoryDto>>(dbCategories).ToList();
            }
            else
            {
                response.Success=false;
                response.Message = "You can not delete a category.";
            }
            return response;
        }

        public async Task<ServiceResponse<List<GetCategoryDto>>> GetAllCategories()
        {
            ServiceResponse<List<GetCategoryDto>> response = new ServiceResponse<List<GetCategoryDto>>();
            List<Category> dbCategories = await _context.Category.ToListAsync();
            response.Data = _mapper.Map<List<GetCategoryDto>>(dbCategories).ToList();
            return response;
        }

        public async Task<ServiceResponse<GetCategoryDto>> GetCategoryById(int id)
        {
            ServiceResponse<GetCategoryDto> response = new ServiceResponse<GetCategoryDto>();
            Category category = await _context.Category.FirstOrDefaultAsync(x => x.Id == id);
            response.Data = _mapper.Map<GetCategoryDto>(category);
            return response;
        }

        public async Task<ServiceResponse<GetCategoryDto>> UpdateCategory(UpdateCategoryDto updatedCategory)
        {
            ServiceResponse<GetCategoryDto> response = new ServiceResponse<GetCategoryDto>();
            Category category = await _context.Category.FirstOrDefaultAsync(x => x.Id == updatedCategory.Id);
            category.Name = updatedCategory.Name;
             _context.Category.Update(category);
            await _context.SaveChangesAsync();
            response.Data = _mapper.Map<GetCategoryDto>(category);
            return response;
        }
    }
}