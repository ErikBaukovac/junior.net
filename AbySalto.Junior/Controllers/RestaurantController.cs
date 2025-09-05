using AbySalto.Junior.Application.DTO;
using AbySalto.Junior.Application.Interfaces;
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
    }
}
