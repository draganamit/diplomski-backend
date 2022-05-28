using System.Collections.Generic;
using System.Threading.Tasks;
using diplomski_backend.Dtos.Products;
using diplomski_backend.Models;
using diplomski_backend.Services.ProductService;
using Microsoft.AspNetCore.Mvc;

namespace diplomski_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductContorller : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductContorller(IProductService productService)
        {
            _productService = productService;

        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllProducts()
        {
            ServiceResponse<List<GetProductWithUserDto>> response = await _productService.GetAllProduct();
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            ServiceResponse<GetProductWithUserDto> response = await _productService.GetProductById(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto updatedProduct)
        {
            ServiceResponse<GetProductWithUserDto> response = await _productService.UpdateProduct(updatedProduct);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(AddProductDto newProduct)
        {
            ServiceResponse<List<GetProductWithUserDto>> response = await _productService.AddProduct(newProduct);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            ServiceResponse<List<GetProductWithUserDto>> response = await _productService.DeleteProduct(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpGet("GetUserProducts")]
        public async Task<IActionResult> GetAllUserProducts()
        {
            ServiceResponse<List<GetProductWithUserDto>> response = await _productService.GetAllUserProducts();
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPost("GetProductsForIndex")]
        public async Task<IActionResult> SearchProducts(ProductSearchModel searchModel)
        {
            ServiceResponse<List<GetProductWithUserDto>> response = await _productService.GetProducts(searchModel);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}