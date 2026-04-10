using RetailOrderWebsite.Data;
using RetailOrderWebsite.DTOs;
using RetailOrderWebsite.Models;
using RetailOrderWebsite.Services.Interfaces;

namespace RetailOrderWebsite.Services.Implementations
{
    public class PaymentService: IPaymentService
    {
        private readonly AppDbContext _context;

        public PaymentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> ProcessPayment(int userId, PaymentDto dto)
        {
            var order = await _context.Orders.FindAsync(dto.OrderId);

            if (order == null)
                throw new Exception("Order not found");

            if (order.UserId != userId)
                throw new Exception("Unauthorized");

            // Simulate payment success
            var payment = new Payment
            {
                OrderId = order.Id,
                Amount = order.TotalAmount,
                PaymentMethod = dto.PaymentMethod,
                Status = "Success"
            };

            // Update order status
            order.Status = "Paid";

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return "Payment Successful";
        }
    }
}
