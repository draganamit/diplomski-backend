using System.Collections.Generic;
using System.Threading.Tasks;
using diplomski_backend.Dtos.Products;
using diplomski_backend.Models;

namespace diplomski_backend.Services.ProductService
{
    public interface IProductService
    {
         public Task<ServiceResponse<List<GetProductDto>>> GetAllProduct();
         public Task<ServiceResponse<GetProductDto>> GetProductById(int id);
         public Task<ServiceResponse<List<GetProductDto>>> AddProduct(AddProductDto newProduct);
         public Task<ServiceResponse<GetProductDto>> UpdateProduct(UpdateProductDto updatedProduct);
         public Task<ServiceResponse<List<GetProductDto>>> DeleteProduct(int id);

    }
}