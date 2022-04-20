using System.Collections.Generic;
using System.Threading.Tasks;
using diplomski_backend.Dtos.Products;
using diplomski_backend.Models;

namespace diplomski_backend.Services.ProductService
{
    public class ProductService : IProductService
    {
        public Task<ServiceResponse<List<GetProductDto>>> AddProduct(AddProductDto newProduct)
        {
            throw new System.NotImplementedException();
        }

        public Task<ServiceResponse<List<GetProductDto>>> DeleteProduct(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<ServiceResponse<List<GetProductDto>>> GetAllProduct()
        {
            throw new System.NotImplementedException();
        }

        public Task<ServiceResponse<GetProductDto>> GetProductById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<ServiceResponse<GetProductDto>> UpdateProduct(UpdateProductDto updatedProduct)
        {
            throw new System.NotImplementedException();
        }
    }
}