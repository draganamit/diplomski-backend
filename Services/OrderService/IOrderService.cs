using System.Collections.Generic;
using System.Threading.Tasks;
using diplomski_backend.Dtos.Orders;
using diplomski_backend.Models;

namespace diplomski_backend.Services.OrderService
{
    public interface IOrderService
    {
        public Task<ServiceResponse<List<GetOrderDto>>> AddOrder(AddOrderDto newOrder);
        public Task<ServiceResponse<List<GetOrderDto>>> GetAllOrdersByUser();
        public Task<ServiceResponse<List<GetOrderDto>>> GetAllOrdersForUser();
        public Task<ServiceResponse<GetOrderDto>> GetOrderById(int id);




    }
}