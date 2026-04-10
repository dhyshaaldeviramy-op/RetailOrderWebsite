using Microsoft.EntityFrameworkCore;
using RetailOrderWebsite.Data;
using RetailOrderWebsite.DTOs.OrderDtos;
using RetailOrderWebsite.Models;
using RetailOrderWebsite.Services.Interfaces;

namespace RetailOrderWebsite.Services.Implementations
{
    public class OrderService:IOrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        // ✅ 1. PLACE ORDER
        public async Task<OrderResponseDto> PlaceOrder(int userId, OrderCreateDto dto)
        {
            var order = new Order
            {
                UserId = userId,
                Status = "Pending",
                CreatedAt = DateTime.Now,
                Items = new List<OrderItem>()
            };

            decimal totalAmount = 0;

            foreach (var item in dto.Items)
            {
                var product = await _context.Products.FindAsync(item.ProductId);
                if (product == null)
                    throw new Exception("Product not found");

                if (product.Stock < item.Quantity)
                    throw new Exception($"Not enough stock for {product.Name}");

                // Reduce stock
                product.Stock -= item.Quantity;

                var orderItem = new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    Price = product.Price
                };

                totalAmount += product.Price * item.Quantity;
                order.Items.Add(orderItem);
            }

            order.TotalAmount = totalAmount;

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // Return response DTO
            return new OrderResponseDto
            {
                Id = order.Id,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                Items = order.Items.Select(i => new OrderItemDetailDto
                {
                    ProductName = _context.Products.First(p => p.Id == i.ProductId).Name,
                    Quantity = i.Quantity,
                    Price = i.Price
                }).ToList()
            };
        }

        // ✅ 2. GET ORDERS
        public async Task<List<OrderResponseDto>> GetOrders(int userId, string role)
        {
            IQueryable<Order> query = _context.Orders
                .Include(o => o.Items);

            // If customer → only their orders
            if (role == "Customer")
            {
                query = query.Where(o => o.UserId == userId);
            }

            var orders = await query.ToListAsync();

            return orders.Select(o => new OrderResponseDto
            {
                Id = o.Id,
                TotalAmount = o.TotalAmount,
                Status = o.Status,
                Items = o.Items.Select(i => new OrderItemDetailDto
                {
                    ProductName = _context.Products.First(p => p.Id == i.ProductId).Name,
                    Quantity = i.Quantity,
                    Price = i.Price
                }).ToList()
            }).ToList();
        }

        // ✅ 3. UPDATE ORDER STATUS (ADMIN)
        public async Task<bool> UpdateOrderStatus(int orderId, string status)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
                return false;

            order.Status = status;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
