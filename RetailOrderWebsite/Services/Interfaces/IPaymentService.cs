using RetailOrderWebsite.DTOs;

namespace RetailOrderWebsite.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<string> ProcessPayment(int userId, PaymentDto dto);
    }
}
