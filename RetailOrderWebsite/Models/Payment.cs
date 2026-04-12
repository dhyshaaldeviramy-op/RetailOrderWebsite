using System.ComponentModel.DataAnnotations;

namespace RetailOrderWebsite.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public decimal Amount { get; set; }

        public string PaymentMethod { get; set; } // Cash / Card / UPI

        public string Status { get; set; } // Pending, Success, Failed

        public DateTime PaidAt { get; set; } = DateTime.Now;
    }
}
