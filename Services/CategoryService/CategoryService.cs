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
        public Task<ServiceResponse<List<GetCategoryDto>>> AddCategory(AddCategoryDto newCategory)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ServiceResponse<List<GetCategoryDto>>> GetAllCategories()
        {
            ServiceResponse<List<GetCategoryDto>> response = new ServiceResponse<List<GetCategoryDto>>();
            List<Category> dbCategories = await _context.Category.ToListAsync();
            response.Data = _mapper.Map<List<GetCategoryDto>>(dbCategories).ToList();
            return response;
        }

        public Task<ServiceResponse<GetCategoryDto>> GetCategoryById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<ServiceResponse<GetCategoryDto>> UpdateCategory(UpdateCategoryDto updatedCategory)
        {
            throw new System.NotImplementedException();
        }
    }
}