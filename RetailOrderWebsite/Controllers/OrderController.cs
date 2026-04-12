using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RetailOrderWebsite.DTOs.OrderDtos;
using RetailOrderWebsite.Services.Interfaces;
using System.Security.Claims;

namespace RetailOrderWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // ✅ 1. PLACE ORDER
        // POST: /api/orders
        [HttpPost]
        public async Task<IActionResult> PlaceOrder([FromBody] OrderCreateDto dto)
        {
            try
            {
                // Get userId from JWT token
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                var result = await _orderService.PlaceOrder(userId, dto);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // ✅ 2. GET ORDERS
        // GET: /api/orders
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var role = User.FindFirst(ClaimTypes.Role)?.Value;

                var orders = await _orderService.GetOrders(userId, role);

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
