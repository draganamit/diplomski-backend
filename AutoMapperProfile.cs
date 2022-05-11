using AutoMapper;
using diplomski_backend.Dtos;
using diplomski_backend.Dtos.Categories;
using diplomski_backend.Dtos.Products;
using diplomski_backend.Models;

namespace backend_user_post
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Category, GetCategoryDto>();
            CreateMap<AddCategoryDto, Category>();
            CreateMap<User, GetUserDto>().ReverseMap();
            CreateMap<User, GetUserWithProductDto>();
            CreateMap<Product, GetProductDto>();
            CreateMap<Product, GetProductWithUserDto>();
            CreateMap<AddProductDto, Product>();
        }
    }
}