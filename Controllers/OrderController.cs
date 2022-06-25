using System.Collections.Generic;
using System.Threading.Tasks;
using diplomski_backend.Dtos.Orders;
using diplomski_backend.Models;
using diplomski_backend.Services.OrderService;
using Microsoft.AspNetCore.Mvc;

namespace diplomski_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;

        }
        [HttpGet("GetByUser")]
        public async Task<IActionResult> GetAllOrdersByUser()
        {
            ServiceResponse<List<GetOrderDto>> response = await _orderService.GetAllOrdersByUser();
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        [HttpGet("GetForUser")]
        public async Task<IActionResult> GetAllOrdersForUser()
        {
            ServiceResponse<List<GetOrderDto>> response = await _orderService.GetAllOrdersForUser();
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

    }
}