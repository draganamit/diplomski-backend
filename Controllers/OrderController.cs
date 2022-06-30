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
        [HttpPost]
        public async Task<IActionResult> AddOrder(AddOrderDto newOrder)
        {
            ServiceResponse<List<GetOrderDto>> response = await _orderService.AddOrder(newOrder);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> SetConfirm(setConfirmDto newConfirm)
        {
            ServiceResponse<GetOrderDto> response = await _orderService.SetConfirm(newConfirm);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            ServiceResponse<List<GetOrderDto>> response = await _orderService.DeleteOrder(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            ServiceResponse<GetOrderDto> response = await _orderService.GetOrderById(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
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