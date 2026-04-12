using RetailOrderWebsite.Models;
using RetailOrderWebsite.Services.Interface;
using System.Collections.Generic;
using System.Linq;
namespace RetailOrderWebsite.Services
{
    public class CartService:ICartService
    {
        private static List<Cart> carts = new List<Cart>();

        public Cart GetCart(int userId)
        {
            return carts.FirstOrDefault(c => c.UserId == userId);
        }

        public Cart AddToCart(Cart newCart)
        {
            var existingCart = carts.FirstOrDefault(c => c.UserId == newCart.UserId);

            if (existingCart != null)
            {
                foreach (var item in newCart.Items)
                {
                    
                    var existingItem = existingCart.Items
                        .FirstOrDefault(i => i.ProductId == item.ProductId);

                    if (existingItem != null)
                    {
                        existingItem.Quantity += item.Quantity;
                    }
                    else
                    {
                        existingCart.Items.Add(item);
                    }
                }
                return existingCart;
            }

            carts.Add(newCart);
            return newCart;
        }

        public Cart RemoveFromCart(int userId, int itemId)
        {
            var cart = carts.FirstOrDefault(c => c.UserId == userId);

            if (cart == null)
                return null;

            var item = cart.Items.FirstOrDefault(i => i.Id == itemId);

            if (item == null)
                return cart;

            cart.Items.Remove(item);

            return cart;
        }
    }
}
