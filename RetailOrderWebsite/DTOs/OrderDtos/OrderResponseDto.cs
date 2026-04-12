namespace RetailOrderWebsite.DTOs.OrderDtos
{
    public class OrderResponseDto
    {
        public int Id { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public List<OrderItemDetailDto> Items { get; set; }
    }
}
