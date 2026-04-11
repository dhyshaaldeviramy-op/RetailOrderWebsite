using RetailOrderWebsite.Models;

namespace RetailOrderWebsite.Services.Interface
{
    public interface ICartService
    {
        Cart GetCart(int userId);
        Cart AddToCart(Cart cart);
        Cart RemoveFromCart(int userId, int itemId);

    }
}
