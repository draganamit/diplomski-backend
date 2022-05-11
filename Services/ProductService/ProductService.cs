using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using diplomski_backend.Data;
using diplomski_backend.Dtos.Products;
using diplomski_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace diplomski_backend.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ProductService(IMapper mapper, DataContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _context = context;
            _mapper = mapper;

        }
        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        public async Task<ServiceResponse<List<GetProductWithUserDto>>> AddProduct(AddProductDto newProduct)
        {
            ServiceResponse<List<GetProductWithUserDto>> response = new ServiceResponse<List<GetProductWithUserDto>>();
            try
            {
                Product product = _mapper.Map<Product>(newProduct);
                product.User = await _context.User.FirstOrDefaultAsync( u => u.Id == GetUserId());
                await _context.Product.AddAsync(product);
                await _context.SaveChangesAsync();
                //List<Product> dbProducts = await _context.Product.ToListAsync();
                //response.Data = _mapper.Map<List<GetProductDto>>(dbProducts).ToList();
                response.Data = (_context.Product.Where(p => p.User.Id == GetUserId()).Select(p => _mapper.Map<GetProductWithUserDto>(p))).ToList();
               // response.Data = null;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<List<GetProductWithUserDto>>> DeleteProduct(int id)
        {
            ServiceResponse<List<GetProductWithUserDto>> response = new ServiceResponse<List<GetProductWithUserDto>>();
            try
            {
                Product product = await _context.Product.FirstOrDefaultAsync(p => p.Id == id && p.User.Id == GetUserId());
                if(product != null)
                {
                    _context.Product.Remove(product);
                    await _context.SaveChangesAsync();
                    //List<Product> products = await _context.Product.ToListAsync();
                    //response.Data = _mapper.Map<List<GetProductDto>>(products).ToList();
                    response.Data = (_context.Product.Where(p => p.User.Id == GetUserId()).Select(p => _mapper.Map<GetProductWithUserDto>(p))).ToList();
                }
                else
                {
                    response.Success=false;
                    response.Message="Product not found.";
                }
                
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<List<GetProductWithUserDto>>> GetAllProduct()
        {
            ServiceResponse<List<GetProductWithUserDto>> response = new ServiceResponse<List<GetProductWithUserDto>>();
            try
            {
                List<Product> products = await _context.Product.Include(p => p.User).ToListAsync();
                response.Data = _mapper.Map<List<GetProductWithUserDto>>(products).ToList();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<GetProductWithUserDto>> GetProductById(int id)
        {
            ServiceResponse<GetProductWithUserDto> response = new ServiceResponse<GetProductWithUserDto>();
            try
            {
                Product product = await _context.Product.FirstOrDefaultAsync(x => x.Id == id);
                response.Data = _mapper.Map<GetProductWithUserDto>(product);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<GetProductWithUserDto>> UpdateProduct(UpdateProductDto updatedProduct)
        {
            ServiceResponse<GetProductWithUserDto> response = new ServiceResponse<GetProductWithUserDto>();
            try
            {
                Product product = await _context.Product.Include(p => p.User).FirstOrDefaultAsync(p => p.Id == updatedProduct.Id);
                if(product.User.Id == GetUserId())
                {
                    product.Name = updatedProduct.Name;
                    product.Description = updatedProduct.Description;
                    product.State = updatedProduct.State;
                    _context.Product.Update(product);
                    await _context.SaveChangesAsync();
                    response.Data = _mapper.Map<GetProductWithUserDto>(product);
                }
                else
                {
                    response.Success=false;
                    response.Message="Product not found";
                }
                
                
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<List<GetProductWithUserDto>>> GetAllUserProducts()
        {
            ServiceResponse<List<GetProductWithUserDto>> response = new ServiceResponse<List<GetProductWithUserDto>>();
            try
            {
                List<Product> products = await _context.Product.Where(p => p.User.Id == GetUserId()).ToListAsync();
                response.Data = (products.Select(p => _mapper.Map<GetProductWithUserDto>(p))).ToList();
                //response.Data = _mapper.Map<List<GetProductDto>>(products).ToList();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}