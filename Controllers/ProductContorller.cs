using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
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
        public async Task<IActionResult> UpdateProduct([FromForm] UpdateProductDto updatedProduct)
        {
            var files = Request.Form.Files;


            for (int i = 0; i < files.Count; i++)
            {
                if (files[i].Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(files[i].ContentDisposition).FileName.Trim('"');
                    var extension = Path.GetExtension(fileName);

                    var diskFileName = Guid.NewGuid().ToString();
                    if (extension != null)
                        diskFileName += extension;

                    var fullPath = Path.Combine("wwwroot/Images", diskFileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        files[i].CopyTo(stream);
                    }
                    updatedProduct.Images.Add(diskFileName);

                    //return Ok(diskFileName);
                }


            }
            ServiceResponse<GetProductWithUserDto> response = await _productService.UpdateProduct(updatedProduct);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromForm] AddProductDto newProduct)
        {
            var files = Request.Form.Files;


            for (int i = 0; i < files.Count; i++)
            {
                if (files[i].Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(files[i].ContentDisposition).FileName.Trim('"');
                    var extension = Path.GetExtension(fileName);

                    var diskFileName = Guid.NewGuid().ToString();
                    if (extension != null)
                        diskFileName += extension;

                    var fullPath = Path.Combine("wwwroot/Images", diskFileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        files[i].CopyTo(stream);
                    }
                    newProduct.Images.Add(diskFileName);

                    //return Ok(diskFileName);
                }


            }

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
            ServiceResponse<List<GetProductWithUserDto>> response = await _productService.SearchProducts(searchModel);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

    }
}