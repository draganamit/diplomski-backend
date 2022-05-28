using System.Collections.Generic;
using System.Threading.Tasks;
using diplomski_backend.Dtos.Products;
using diplomski_backend.Models;

namespace diplomski_backend.Services.ProductService
{
    public interface IProductService
    {
        public Task<ServiceResponse<List<GetProductWithUserDto>>> GetAllProduct();
        public Task<ServiceResponse<GetProductWithUserDto>> GetProductById(int id);
        public Task<ServiceResponse<List<GetProductWithUserDto>>> AddProduct(AddProductDto newProduct);
        public Task<ServiceResponse<GetProductWithUserDto>> UpdateProduct(UpdateProductDto updatedProduct);
        public Task<ServiceResponse<List<GetProductWithUserDto>>> DeleteProduct(int id);
        public Task<ServiceResponse<List<GetProductWithUserDto>>> GetAllUserProducts();
        public Task<ServiceResponse<List<GetProductWithUserDto>>> GetProducts(ProductSearchModel searchModel);


    }
}