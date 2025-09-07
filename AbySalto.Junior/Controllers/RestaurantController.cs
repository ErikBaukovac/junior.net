using AbySalto.Junior.Application.DTO;
using AbySalto.Junior.Application.Interfaces;
using AbySalto.Junior.Models;
using Microsoft.AspNetCore.Mvc;

namespace AbySalto.Junior.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RestaurantController : Controller
    {
        private readonly IRestaurantService _service;

        public RestaurantController(IRestaurantService service)
        {
            _service = service;
        }

        [HttpGet("GetOrders")]
        public async Task<List<OrderModel>> GetOrders() 
        {
            var orders = await _service.GetOrders();
            return orders.ToList();
        }

        [HttpPost("AddOrder")]
        public async Task<IActionResult> AddOrderAsync([FromBody] OrderModel order)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var orderID = await _service.AddOrderAsync(order);

            return Ok(orderID);
        }

        [HttpGet("ChangeStatus")]
        public async Task<IActionResult> ChangeOrderStatus(int orderId, int statusId)
        {
            var successful = await _service.ChangeOrderStatus(orderId, statusId);

            if(!successful)
                return BadRequest(ModelState);
            else
                return Ok();

        }

        [HttpGet("SortByValue")]
        public async Task<List<OrderModel>> SortOrdersByValue()
        {
            var orders = await _service.SortOrdersByValue();
            return orders.ToList();
        }

        [HttpGet("GetTotalValue")]
        public async Task<decimal> GetTotalOrdersValue(int userId)
        {
            var total = await _service.GetTotalOrdersValue(userId);
            return total;
        }
    }
}
