using System.Collections.Generic;
using System.Threading.Tasks;
using diplomski_backend.Dtos;
using diplomski_backend.Dtos.Orders;
using diplomski_backend.Models;

namespace diplomski_backend.Services.OrderService
{
    public interface IOrderService
    {
        public Task<ServiceResponse<List<GetOrderDto>>> AddOrder(AddOrderDto newOrder);
        public Task<ServiceResponse<List<GetOrderDto>>> GetAllOrdersByUser(OrderPageModel pageModel);
        public Task<ServiceResponse<List<GetOrderDto>>> GetAllOrdersForUser(OrderPageModel pageModel);
        public Task<ServiceResponse<GetOrderDto>> GetOrderById(int id);
        public Task<ServiceResponse<GetOrderDto>> SetConfirm(SetConfirmDto newConfirm);
        public Task<ServiceResponse<GetOrderDto>> SetRefuse(SetRefuseDto newRefuse);

        public Task<ServiceResponse<List<GetOrderDto>>> DeleteOrder(int id);
        public Task<ServiceResponse<List<GetOrderDto>>> SearchOrders(OrderSearchModel reportSearchModel);





    }
}