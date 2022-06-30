using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using diplomski_backend.Data;
using diplomski_backend.Dtos.Orders;
using diplomski_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace diplomski_backend.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public OrderService(IMapper mapper, DataContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _context = context;
            _mapper = mapper;

        }
        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<ServiceResponse<List<GetOrderDto>>> AddOrder(AddOrderDto newOrder)
        {
            ServiceResponse<List<GetOrderDto>> response = new ServiceResponse<List<GetOrderDto>>();
            try
            {
                Order order = _mapper.Map<Order>(newOrder);
                order.Product = await _context.Product.FirstOrDefaultAsync(p => p.Id == newOrder.ProductId);
                order.UserBuyer = await _context.User.FirstOrDefaultAsync(u => u.Id == GetUserId());
                await _context.Order.AddAsync(order);
                await _context.SaveChangesAsync();
                response.Data = (_context.Order.Where(p => p.UserBuyer.Id == GetUserId()).Select(p => _mapper.Map<GetOrderDto>(p))).ToList();

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;

        }

        public async Task<ServiceResponse<List<GetOrderDto>>> GetAllOrdersByUser()
        {
            ServiceResponse<List<GetOrderDto>> response = new ServiceResponse<List<GetOrderDto>>();
            try
            {
                List<Order> orders = await _context.Order.Where(p => p.UserBuyer.Id == GetUserId()).Include(p => p.Product).Include(p => p.Product.User).ToListAsync();
                response.Data = _mapper.Map<List<GetOrderDto>>(orders).ToList();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<List<GetOrderDto>>> GetAllOrdersForUser()
        {
            ServiceResponse<List<GetOrderDto>> response = new ServiceResponse<List<GetOrderDto>>();
            try
            {
                List<Order> orders = await _context.Order.Where(p => p.Product.User.Id == GetUserId()).Include(p => p.Product).Include(p => p.UserBuyer).ToListAsync();
                response.Data = _mapper.Map<List<GetOrderDto>>(orders).ToList();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
        public async Task<ServiceResponse<GetOrderDto>> GetOrderById(int id)
        {
            ServiceResponse<GetOrderDto> response = new ServiceResponse<GetOrderDto>();
            try
            {
                Order order = await _context.Order.Include(p => p.Product).Include(p => p.UserBuyer).Include(p => p.Product.User).FirstOrDefaultAsync(o => o.Id == id);

                response.Data = _mapper.Map<GetOrderDto>(order);

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }



        public async Task<ServiceResponse<GetOrderDto>> SetConfirm(setConfirmDto newConfirm)
        {
            ServiceResponse<GetOrderDto> response = new ServiceResponse<GetOrderDto>();
            try
            {
                Order order = await _context.Order.Include(p => p.Product).Include(p => p.UserBuyer).Include(p => p.Product.User).FirstOrDefaultAsync(o => o.Id == newConfirm.IdOrder);
                order.Confirm = newConfirm.Confirm;
                _context.Order.Update(order);
                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetOrderDto>(order);

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;

        }

        public async Task<ServiceResponse<List<GetOrderDto>>> DeleteOrder(int id)
        {
            ServiceResponse<List<GetOrderDto>> response = new ServiceResponse<List<GetOrderDto>>();
            try
            {
                Order order = await _context.Order.FirstOrDefaultAsync(o => o.Id == id);
                if (order != null)
                {
                    _context.Order.Remove(order);
                    await _context.SaveChangesAsync();

                    List<Order> orders = await _context.Order.Include(p => p.Product).Include(p => p.UserBuyer).Include(p => p.Product.User).ToListAsync();
                    response.Data = _mapper.Map<List<GetOrderDto>>(orders).ToList();
                }
                else
                {
                    response.Success = false;
                    response.Message = "Product not found.";
                }

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