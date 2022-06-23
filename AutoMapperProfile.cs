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
            CreateMap<Product, GetProductWithUserDto>()
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags.Split(";", System.StringSplitOptions.None)));
            CreateMap<AddProductDto, Product>();
            CreateMap<AddProductDto, Product>()
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => string.Join(";", src.Tags)));
            CreateMap<UpdateProductDto, Product>()
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => string.Join(";", src.Tags)));

        }
    }
}