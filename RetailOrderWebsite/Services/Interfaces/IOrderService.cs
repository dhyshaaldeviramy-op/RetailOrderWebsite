using RetailOrderWebsite.DTOs.OrderDtos;

namespace RetailOrderWebsite.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderResponseDto> PlaceOrder(int userId, OrderCreateDto dto);
        Task<List<OrderResponseDto>> GetOrders(int userId, string role);
        Task<bool> UpdateOrderStatus(int orderId, string status);
    }
}
