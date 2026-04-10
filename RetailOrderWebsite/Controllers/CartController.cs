using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using RetailOrderWebsite.Data;
using RetailOrderWebsite.Models;


namespace RetailApp.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class CartController : ControllerBase
        {
            // Temporary memory storage (replace with DB later)
            private static List<Cart> carts = new List<Cart>();

            [HttpGet("{userId}")]
            public IActionResult GetCart(int userId)
            {
                var cart = carts.FirstOrDefault(c => c.UserId == userId);

                if (cart == null)
                    return NotFound("Cart not found");

                return Ok(cart);
            }

            [HttpPost("add")]
            public IActionResult AddToCart([FromBody] Cart newCart)
            {
                var existingCart = carts.FirstOrDefault(c => c.UserId == newCart.UserId);

                if (existingCart != null)
                {
                    // Add items to existing cart
                    foreach (var item in newCart.Items)
                    {
                        existingCart.Items.Add(item);
                    }
                    return Ok(existingCart);
                }

                // Create new cart
                carts.Add(newCart);
                return Ok(newCart);
            }

            [HttpDelete("remove/{id}")]
            public IActionResult RemoveItem(int id)
            {
                var cart = carts.FirstOrDefault();

                if (cart == null)
                    return NotFound("Cart not found");

                var item = cart.Items.FirstOrDefault(i => i.Id == id);

                if (item == null)
                    return NotFound("Item not found");

                cart.Items.Remove(item);

                return Ok(cart);
            }
        }
    }